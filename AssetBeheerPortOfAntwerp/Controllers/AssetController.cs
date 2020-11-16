using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;
using BLL.interfaces;
using BLL.ViewModels;
using NinjaNye.SearchExtensions;
using Microsoft.AspNetCore.Authorization;


namespace PortOfAntwerpAppAssets.Controllers
{
   
    //[Authorize(Policy = "ReadClaimPolicy")]

    public class AssetController : Controller
    {
        private readonly IAssetService service;
        private readonly IPurchaseItemService servicePurchaseItem;

        public AssetController(IAssetService _service, IPurchaseItemService _servicePurchaseItem)
        {
            service = _service;
            servicePurchaseItem = _servicePurchaseItem;
        }


        // GET: Asset
        //public IActionResult Index()
        //{

        //    //IEnumerable<object> assets2 = service.GetOperationalSiteGrid();
        //    List<Asset> assets = service.GetAllAssets();

        //    return View(assets);
        //}


        //GET: Asset
        
        public IActionResult Index(string sortOrder, string searchString, bool qrCodeSwitch,  bool outSwitch)
        {
            //IEnumerable<object> assets2 = service.GetOperationalSiteGrid();
            qrCodeSwitch = true;
            //ViewData["AssetPivot"] = assets2;

            ViewData["AssetTagSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["PurchaseItemSortParm"] = sortOrder == "PurchaseItem" ? "purchaseItem_desc" : "PurchaseItem";
            ViewData["OwnerSortParm"] = sortOrder == "Owner" ? "owner_desc" : "Owner";
            ViewData["LocationSortParm"] = sortOrder == "Location" ? "location_desc" : "Location";
            ViewData["WarningDateSortParm"] = sortOrder == "WarningDate" ? "warningDate_desc" : "WarningDate";
            ViewData["ExpireDateSortParm"] = sortOrder == "ExpireDate" ? "expireDate_desc" : "ExpireDate";
            ViewData["NoSupportSortParm"] = sortOrder == "NoSupport" ? "noSupport_desc" : "NoSupport";
            ViewData["CurrentFilter"] = searchString;
            ViewData["qrCodeSwitch"] = qrCodeSwitch;
            ViewData["outSwitch"] = outSwitch;

            //if (string.IsNullOrEmpty(searchString))
            //{
            //    searchString = "";
            //}

            IEnumerable<Asset> assets = service.GetAllAssetsEnum(searchString, outSwitch);

            switch (sortOrder)
            {
                case "name_desc":
                    assets = assets.OrderByDescending(s => s.AssetTag != null ? s.AssetTag.ToString().Substring(8) : "").ThenByDescending(s => s.AssetTag != null ? s.AssetTag.ToString().Substring(0, 8) : "");
                    break;
                case "Status":
                    assets = assets.OrderBy(s => s.Status.Name);
                    break;
                case "status_desc":
                    assets = assets.OrderByDescending(s => s.Status.Name);
                    break;
                case "PurchaseItem":
                    assets = assets.OrderBy(s => s.PurchaseItem.Product.Name);
                    break;
                case "purchaseItem_desc":
                    assets = assets.OrderByDescending(s => s.PurchaseItem.Product.Name);
                    break;
                case "Owner":
                    assets = assets.OrderBy(s => s.AssetOwner.OwnerDescription);
                    break;
                case "owner_desc":
                    assets = assets.OrderByDescending(s => s.AssetOwner.OwnerDescription);
                    break;
                case "Location":
                    assets = assets.OrderBy(s => s.OperationalSiteLocation != null ? s.OperationalSiteLocation.Location.LocationDescription : "");
                    break;
                case "location_desc":
                    assets = assets.OrderByDescending(s => s.OperationalSiteLocation != null ? s.OperationalSiteLocation.Location.LocationDescription : "");
                    break;
                case "WarningDate":
                    assets = assets.OrderBy(s => s.WarningDate);
                    break;
                case "warningDate_desc":
                    assets = assets.OrderByDescending(s => s.WarningDate);
                    break;
                case "ExpireDate":
                    assets = assets.OrderBy(s => s.ExpireDate);
                    break;
                case "expireDate_desc":
                    assets = assets.OrderByDescending(s => s.ExpireDate);
                    break;
                case "NoSupport":
                    assets = assets.OrderByDescending(s => s.PurchaseItem.Product.Status.NoSupport);
                    break;
                case "noSupport_desc":
                    assets = assets.OrderBy(s => s.PurchaseItem.Product.Status.NoSupport);
                    break;
                default:
                    assets = assets.OrderBy(s => s.AssetTag != null ? s.AssetTag.ToString().Substring(8) : "").ThenBy(s => s.AssetTag != null ? s.AssetTag.ToString().Substring(0, 8) : "");
                    break;
            }



            return View(assets);
        }


        // GET: Asset/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Asset asset = service.FindById(id.Value);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Asset/CreateTagNummer
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult CreateTagNumber(long purchaseItemID)
        {
            PurchaseItem purchaseItem = servicePurchaseItem.FindById(purchaseItemID);

            ViewData["OldTagNumber"] = "";

            return View(purchaseItem);
        }

        // POST: Asset/CreateTagNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult CreateTagNumber(long purchaseItemID, int gotTagNumber)
        {
            string oldTagNumber = "";

            if (gotTagNumber != null)
            {
                Tuple<long, string, bool, string> tagNumber = service.CheckIfTagNumberExists(purchaseItemID, gotTagNumber);

                if (tagNumber.Item3 == false)
                {
                    return RedirectToAction("Create", "Asset", new { purchaseItemID, assetTag = tagNumber.Item2 });
                }
                else
                {
                    oldTagNumber = tagNumber.Item2;
                }
            }

            PurchaseItem purchaseItem = servicePurchaseItem.FindById(purchaseItemID);

            ViewData["OldTagNumber"] = oldTagNumber;

            return View(purchaseItem);

        }


        // GET: Asset/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long purchaseItemID, string assetTag)
        {
            Tuple<Asset, int> asset = service.CreateNewAsset(purchaseItemID, assetTag);


            ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
            ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
            ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());

            ViewData["QtyAssetsStillNeedToAdd"] = asset.Item2;
            
            return View(asset.Item1);
        }

        // POST: Asset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long purchaseItemID, [Bind("AssetID,ItemID,StatusID,AssetTag,SerieNo,ServiceTag,TargetDescription,DeliveryDate,ExpirePeriodMonth,WarningPeriodID,WarentyIntervalMonth,UsingDateStart,UsingDateEnd,Remarks,SpecificationFile,AssetOwnerID,OperationalSiteLocationID,RackLocationID,URackID,BackupID")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.PurchaseItemID = purchaseItemID;
                Tuple<Asset, bool, int, string> addAsset = service.Add(asset);
                asset = addAsset.Item1;

                if (addAsset.Item2)
                {
                    return RedirectToAction("CreateTagNumber", "Asset", new { purchaseItemID = asset.PurchaseItemID, qtyAssetsToCreate = addAsset.Item3 });
                }
                return RedirectToAction("Edit", "Purchase", new { id = asset.PurchaseItem.PurchaseID });
            }

            ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
            ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
            ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());

            return View(asset);
        }

        // GET: Asset/CreatAutomatic
        public IActionResult CreatAutomatic(long purchaseItemID, int qtyAdd)
        {
            //In BLL create a lits of Assets and add Qty of PurchaseItems and return the list
            List<Asset> assets = service.CreateAssetsWithPurchaseItem(purchaseItemID, qtyAdd);

            ViewData["PurchaseItemID"] = purchaseItemID;
            ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
            ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
            ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());

            return View(assets.ToList());
        }

        // POST: Asset/CreatAutomatic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatAutomatic([Bind("AssetID,PurchaseItemID,StatusID,AssetTag,SerieNo,ServiceTag,TargetDescription,DeliveryDate,ExpirePeriodMonth,WarningPeriodID,WarentyIntervalMonth,UsingDateStart,UsingDateEnd,Remarks,SpecificationFile,AssetOwnerID,OperationalSiteLocationID,RackLocationID,URackID,BackupID")] List<Asset> assets)
        {
            if (ModelState.IsValid)
            {
                //Saves the returnd List of Assests to database and return the List back filled up.
                assets = service.AddList(assets);
            }

            return RedirectToAction("CreatAutomaticReturn", "Asset", new { purchaseItemID = assets[0].PurchaseItemID });


            //return RedirectToAction("CreatAutomaticReturn");
            //return RedirectToAction(nameof(CreatAutomaticReturn));
        }

        // GET: Asset/CreatAutomaticReturn
        public IActionResult CreatAutomaticReturn(long purchaseItemID)
        {
            List<Asset> assets = service.GetAllAssetsOfPuchaseItem(purchaseItemID);


            ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
            ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
            ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());
            ViewData["PurchaseID"] = assets[0].PurchaseItem.PurchaseID;


            return View(assets);
        }

        // GET: Asset/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Asset asset = service.FindById(id.Value);

            //Get Asset + all subforms info.
            AssetViewModel asset = service.GetSpecificAssetAndAllSubForms(id.Value);
            if (asset == null)
            {
                return NotFound();
            }

            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
            
            //ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
            ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocationsByAssetOwnwer(asset.asset.AssetOwnerID));

            //ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
            if (asset.asset.OperationalSiteLocationID != null)
            {
                ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocationsByOperationalSiteLocation(asset.asset.OperationalSiteLocationID.Value));
            }
            

            ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());

            return View(asset);
        }

        // POST: Asset/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, bool? goToCreateBackup, bool? goToAssetLicense, bool? goToAssetDetail, AssetViewModel assetViewModel, [Bind("AssetID,PurchaseItemID,StatusID,AssetTag,SerieNo,ServiceTag,TargetDescription,DeliveryDate,ExpirePeriodMonth,WarningPeriodID,WarentyIntervalMonth,UsingDateStart,UsingDateEnd,Remarks,SpecificationFile,AssetOwnerID,OperationalSiteLocationID,RackLocationID,URackID,BackupID")] Asset asset)
        {
            if (id != asset.AssetID)
            {
                return NotFound();
            }

            // if we want to change the 3x bellow comboboxes back to "null"
            // the data comes back from the view via AssetViewModel but need to be saved to assat,

            // so, a) we need to actualised the Id's in asset and ...
            asset.OperationalSiteLocationID = assetViewModel.asset.OperationalSiteLocationID;
            asset.RackLocationID = assetViewModel.asset.RackLocationID;
            asset.URackID = assetViewModel.asset.URackID;

            // ... b) we need to switch the ModelState.
            ModelState.Clear();
            TryValidateModel(asset);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(asset);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (goToCreateBackup == true)
                {
                    return RedirectToAction("Create", "Backup", new { assetID = asset.AssetID });
                }
                else if (goToAssetLicense == true)
                {
                    return RedirectToAction("AllUnusedLicenses", "License", new { assetID = asset.AssetID });
                }
                else if (goToAssetDetail == true)
                {
                    return RedirectToAction("Create", "AssetDetail", new { assetID = asset.AssetID });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
            ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
            ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());

            return View(asset);
        }

        [HttpPost]
        public ActionResult OperationalSiteLocationByOwner(long assetOwnerID)
        {
            var operationalSiteLocations = service.GetOperationalSiteLocationsByOwner(assetOwnerID)
                            .Select(x => new
                            {
                                id = x.OperationalSiteLocationID,
                                location = x.Location.LocationDescription
                            }).ToArray();

            //var operationalSiteLocations = (from o in service.GetOperationalSiteLocationsByOwner(assetOwnerID)
            //                                select new
            //                                {
            //                                    id = o.OperationalSiteLocationID,
            //                                    location = o.Location.LocationDescription
            //                                }).ToArray();

            return Json(operationalSiteLocations);
        }

        //public ActionResult GetSubOperationalSiteLocation(long assetOwnerID)
        //{

        //    var operationalSiteLocations = service.GetOperationalSiteLocationsByOwner(assetOwnerID)
        //                    .Select(x => new 
        //                    {
        //                        id = x.OperationalSiteLocationID,
        //                        location = x.Location.LocationDescription
        //                    }).ToArray();

        //    return Json(operationalSiteLocations);
        //}

        [HttpPost]
        public ActionResult GetRackLocationsIDByOperationalSiteLocation(long operationalSiteLocationID)
        {
            var rackLocations = service.GetRackLocationsIDByOperationalSiteLocation(operationalSiteLocationID)
                            .Select(x => new
                            {
                                id = x.RackLocationID,
                                rack = x.RackNo + " - " + x.Rack.Name 
                            }).ToArray();

            return Json(rackLocations);
        }

        // GET: Asset/EditList
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult EditList(long purchaseID, long purchaseItemID)
        {
            if (purchaseItemID == 0)
            {
                return NotFound();
            }

            List<Asset> assets = service.GetAllAssetsOfPuchaseItem(purchaseItemID);

            if (assets != null)
            {
                ViewData["AssetOwnerID"] = new List<SelectListItem>(service.GetSelectListAssetOwners());
                ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusAsset());
                ViewData["OperationalSiteLocationID"] = new List<SelectListItem>(service.GetSelectListOperationalSiteLocations());
                ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
                ViewData["RackLocationID"] = new List<SelectListItem>(service.GetSelectListRackLocations());
                ViewData["URackID"] = new List<SelectListItem>(service.GetSelectListURacks());
                ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());
                ViewData["PurchaseID"] = assets[0].PurchaseItem.PurchaseID;


                return View(assets);
            }

            return RedirectToAction("Edit", "Purchase", new { id = purchaseID });


        }

        // POST: Asset/EditList
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult EditList(long purchaseID, [Bind("AssetID,PurchaseItemID,StatusID,AssetTag,SerieNo,ServiceTag,TargetDescription,DeliveryDate,ExpirePeriodMonth,WarningPeriodID,WarentyIntervalMonth,UsingDateStart,UsingDateEnd,Remarks,SpecificationFile,AssetOwnerID,OperationalSiteLocationID,RackLocationID,URackID,BackupID")] List<Asset> assets)
        {
            if (ModelState.IsValid)
            {
                //Saves the returnd List of Assests to database and return the List back filled up.
                assets = service.UpdateList(assets);
            }

            return RedirectToAction("Edit", "Purchase", new { id = purchaseID });

        }

        // GET: Asset/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tuple<long, Asset, List<Backup>, List<AssetDetail>, List<License>> asset = service.GetAssetWithSubs(id.Value);

            if (asset.Item2 == null)
            {
                return RedirectToAction("Edit", new { id });
            }

            int qtyBackups = asset.Item3.Count();
            int qtyAssetDetails = asset.Item4.Count();
            int qtyLicenses = asset.Item5.Count();

            int qty = qtyBackups + qtyAssetDetails + qtyLicenses;

            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyBackups"] = qtyBackups != 0 ? qtyBackups.ToString() : "0";
            ViewData["ListBackups"] = new List<Backup>(asset.Item3);

            ViewData["QtyAssetDetails"] = qtyAssetDetails != 0 ? qtyAssetDetails.ToString() : "0";
            ViewData["ListAssetDetails"] = new List<AssetDetail>(asset.Item4);

            ViewData["QtyLicenses"] = qtyLicenses != 0 ? qtyLicenses.ToString() : "0";
            ViewData["ListLicenses"] = new List<License>(asset.Item5);

            return View(asset.Item2);
        }

        // POST: Asset/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(long id)
        {
            return service.AssetExists(id);
        }

        // GET: AssetsNeedBackup        
        public IActionResult AssetsNeedBackup()
        {
            List<Asset> assets = service.GetAllAssetsNeedBackUp();

            return View(assets);
        }


        //GET: AssetsOverview        
        public IActionResult AssetsOverview()
        {
            //IEnumerable<object> assets = service.GetOperationalSiteGrid();

            //ViewData["OperationalSiteGrid"] = assets;

            IEnumerable<object> assets = service.GetOperationalSiteGrid();

            ViewData["AssetPivot"] = assets;

            return View();
        }



        [HttpPost]
        public ActionResult LocationsByAssetOwner(long id)
        {

            List<SelectListItem> operationalSiteLocations = service.GetSelectListOperationalSiteLocationsByAssetOwner(id);


            // Filter the states by country. For example:
            var list = (from s in operationalSiteLocations
                          where s.Value == id.ToString()
                          select new
                          {
                              id = s.Value,
                              name = s.Text
                          }).ToArray();

            //return Json(new SelectList(operationalSiteLocations, "Value", "Text"));
            return Json(list);
        }

        
        [HttpPost]
        public ActionResult GetAssetOwner(long assetOwnerID)
        {
            AssetOwner assetOwner = service.GetAssetOwner(assetOwnerID);

            return Json(assetOwner);
        }

        public JsonResult OnGetAssetPivotData()
        {
            //assets = service.GetAllAssets();

            List<ListAssetsPivotData> assets = service.GetAllAssetsPivotData();

            //ViewData["AssetPivotData"] = assets;

            return new JsonResult(assets);
        }

    }
}
