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
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class LicenseController : Controller
    {
        private readonly ILicenseService service;

        public LicenseController(ILicenseService _service)
        {
            service = _service;
        }

        // GET: License
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["PurchaseItemSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["NoSortParm"] = sortOrder == "No" ? "no_desc" : "No";
            ViewData["KeySortParm"] = sortOrder == "Key" ? "key_desc" : "Key";
            ViewData["LicenseTypeSortParm"] = sortOrder == "LicenseType" ? "licenseType_desc" : "LicenseType";
            ViewData["LicenseValidityTypeSortParm"] = sortOrder == "LicenseValidityType" ? "licenseValidityType_desc" : "LicenseValidityType";
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<License> licenses = service.GetAllLicensesEnum(searchString);

            switch (sortOrder)
            {
                case "name_desc":
                    licenses = licenses.OrderByDescending(h => h.PurchaseItem.Product.Name ?? "").ThenByDescending(h => h.Status.Name ?? "");
                    break;
                case "Status":
                    licenses = licenses.OrderBy(s => s.Status.Name);
                    break;
                case "status_desc":
                    licenses = licenses.OrderByDescending(s => s.Status.Name);
                    break;
                case "No":
                    licenses = licenses.OrderBy(s => s.No);
                    break;
                case "no_desc":
                    licenses = licenses.OrderByDescending(s => s.No);
                    break;
                case "Key":
                    licenses = licenses.OrderBy(h => h.Key);
                    break;
                case "key_desc":
                    licenses = licenses.OrderByDescending(h => h.Key);
                    break;
                case "LicenseType":
                    licenses = licenses.OrderBy(s => s.LicenseValidityType.Name ??"");
                    break;
                case "licenseType_desc":
                    licenses = licenses.OrderByDescending(s => s.LicenseType.Name ??"");
                    break;
                case "LicenseValidityType":
                    licenses = licenses.OrderBy(h => h.LicenseValidityType.Name ??"");
                    break;
                case "licenseValidityType_desc":
                    licenses = licenses.OrderByDescending(h => h.LicenseValidityType.Name ?? "");
                    break;
                default:
                    licenses = licenses.OrderBy(h => h.PurchaseItem.Product.Name ?? "").ThenBy(h => h.Status.Name != null ? h.Status.Name : "");
                    break;
            }
            //List<License> licenses = service.GetAllLicenses();
            return View(licenses);
        }

        // GET: License/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            License license = service.FindById(id.Value);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // GET: License/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

            return View();
        }

        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("LicenseID,PurchaseItemID,No,Key,HasCol,ColFileName,ValidityTypeTime,LicenseTypeID,LicenseValidityTypeID,AssetID,StatusID,QtyLimited, ParentLicense")] License license)
        {
            if (ModelState.IsValid)
            {
                service.Add(license);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

            return View(license);
        }

        // GET: License/CreatAutomatic
        //ListUnusedLicensesToAddOnAsset
        public IActionResult CreatAutomatic(long purchaseItemID, int qtyAdd)
        {
            //not working well yet
            //In BLL create a lits of Assets and add Qty of PurchaseItems and return the list
            List<License> licenses = service.CreateLicensesWithPurchaseItem(purchaseItemID, qtyAdd);

            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

            return View(licenses);
        }


        // POST: License/CreatAutomatic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatAutomatic([Bind("LicenseID,PurchaseItemID,No,Key,HasCol,ColFileName,ValidityTypeTime,LicenseTypeID,LicenseValidityTypeID,AssetID, StatusID,QtyLimited, ParentLicense")] List<License> licenses)
        {
            License license = new License();

            if (ModelState.IsValid)
            {
                license = service.AddList(licenses);
            }

            return RedirectToAction("Edit", "Purchase", new { id = license.PurchaseItem.PurchaseID});
        }

        // GET: License/SelectSoftwareType
        public IActionResult SelectSoftwareType(long assetID)
        {
            SoftwareType softwareType = new SoftwareType();

            ViewData["AssetID"] = assetID;
            ViewData["SoftwareTypeID"] = new List<SelectListItem>(service.GetSelectListSoftwareTypes());

             return View(softwareType);
        }

        // POST: License/SelectSoftwareType/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult SelectSoftwareType (long assetID, SoftwareType softwareType)
    {
            if ( softwareType.SoftwareTypeID > 0 && assetID > 0 )
            {
                return RedirectToAction("SelectLicense", "License", new { assetID, selectedSoftwareTypeID = softwareType.SoftwareTypeID });
            }

            ViewData["AssetID"] = assetID;
            ViewData["SoftwareTypeID"] = new List<SelectListItem>(service.GetSelectListSoftwareTypes());

            return View(softwareType);
        }

        // GET: License/SelectLicense
        public IActionResult SelectLicense(long assetID, long? selectedSoftwareTypeID)
        {
            //selectedSoftwareTypeID = 1;

            List<License> licenses = new List<License>();

            if (selectedSoftwareTypeID != null)
            {
                licenses = service.GetAllOrSelectedLicenses(selectedSoftwareTypeID.Value);
            }
            else
            {
                return RedirectToAction("SelectSoftwareType", "License", new { assetID });
            }

            

            if (licenses.Count() > 0)
            {
                string softwareTypeName = ((Models.Software)(licenses[0]).PurchaseItem.Product).SoftwareType.Name;
                ViewData["SoftwareType"] = "List of all licenses from the selected software type: " + softwareTypeName;
            }
            else
            {
                ViewData["SoftwareType"] = "The previous selected Software type has no license!";
            }
            

            ViewData["AssetID"] = assetID;
            
            ViewData["SoftwareTypeID"] = new List<SelectListItem>(service.GetSelectListSoftwareTypes());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

            ViewData["SelectedSoftwareTypeID"] = selectedSoftwareTypeID;

            return View(licenses);
        }

        // POST: License/SelectLicense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectLicense(long assetID, long? selectedSoftwareTypeID, [Bind("LicenseID, AssetID")] List<License> licenses)
        {

            List<AssetLicense> assetLicenses = new List<AssetLicense>();

            foreach (var item in licenses)
            {
                AssetLicense assetLicense = new AssetLicense();
                if (item.AddToAsset == true)
                {
                    assetLicense.AssetID = assetID;
                    assetLicense.LicenseID = item.LicenseID;

                    assetLicenses.Add(assetLicense);

                    item.AddToAsset = false;
                }
            }

            if (ModelState.IsValid)
            {
                service.AddListAssetLicenses(assetLicenses);
                return RedirectToAction("Edit", "Asset", new { id = assetID });
            }


            if (selectedSoftwareTypeID != null)
            {
                licenses = service.GetAllOrSelectedLicenses(selectedSoftwareTypeID);
            }


            ViewData["AssetID"] = assetID;
            ViewData["SoftwareTypeID"] = new List<SelectListItem>(service.GetSelectListSoftwareTypes());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

            ViewData["SelectedSoftwareTypeID"] = selectedSoftwareTypeID;

            return View(licenses);
        }


        //// GET: License/AllUnusedLicenses
        //public IActionResult AllUnusedLicenses(long assetID)
        //{
        //    List<License> licenses = service.GetAllUnusedLicenses();

        //    ViewData["AssetID"] = assetID;
        //    //ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
        //    ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
        //    ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
        //    ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
        //    ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

        //    return View(licenses);
        //}

        //// POST: License/AllUnusedLicenses
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AllUnusedLicenses(long assetID, [Bind("LicenseID, AssetID")] List<License> licenses)
        //{

        //    List<AssetLicense> assetLicenses = new List<AssetLicense>();

        //    foreach (var item in licenses)
        //    {
        //        AssetLicense assetLicense = new AssetLicense();
        //        if (item.AddToAsset==true)
        //        {
        //            assetLicense.AssetID = assetID;
        //            assetLicense.LicenseID = item.LicenseID;

        //            assetLicenses.Add(assetLicense);

        //            item.AddToAsset = false;
        //        }
        //    }


        //    if (ModelState.IsValid)
        //    {
        //        service.AddListAssetLicenses(assetLicenses);

        //        return RedirectToAction("Edit", "Asset", new { id = assetID });
        //    }


        //    return RedirectToAction(nameof(Index));
        //}



        // GET: License/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id, long? assetID, long? purchaseItemID)
        {
            if (id == null)
            {
                return NotFound();
            }

            //License license = service.FindById(id.Value);

            Tuple<long, LicenseViewModel> license = service.GetLicenseWithAssets(id.Value);

            if (license.Item2 == null)
            {
                return NotFound();
            }
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());
            ViewData["assetID"] = assetID;
            ViewData["ParentLicenseID"] = new List<SelectListItem>(service.GetSelectListLicenseOfLicenseType(license.Item2.license.LicenseTypeID));
            ViewData["GoToPurchaseItemEdit"] = purchaseItemID;
            return View(license.Item2);
        }

        // POST: License/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, long? assetID, LicenseViewModel licenseVeiwModel ,[Bind("LicenseID,PurchaseItemID,No,Key,HasCol,ColFileName,ValidityTypeTime,LicenseTypeID,LicenseValidityTypeID,AssetID,StatusID,QtyLimited, ParentLicense")] License license)
        {
            if (id != license.LicenseID)
            {
                return NotFound();
            }

            Tuple<string, string, bool> filePath = service.UploadLicenseColPdf(licenseVeiwModel.license.LicenseID, licenseVeiwModel.FileDescription, licenseVeiwModel.File);

            //add FileName and FilePath to license so that this will be saved to the database
            license.ColFileName = filePath.Item1;
            license.ColFilePath = filePath.Item2;
            license.HasCol = filePath.Item3;

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(license);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseExists(license.LicenseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (assetID == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Edit", "Asset", new { id = assetID });
            }

            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseTypes());
            ViewData["LicenseValidityTypeID"] = new List<SelectListItem>(service.GetSelectListLicenseValidityTypes());
            ViewData["PurchaseItemID"] = new List<SelectListItem>(service.GetSelectListPurchaseItems());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusLicense());

            return View(license);
        }

        // GET: License/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tuple<long, LicenseViewModel> license = service.GetLicenseWithAssets(id.Value);

            if (license.Item2 == null)
            {
                return RedirectToAction("Edit", new { id });
            }

            int qty = license.Item2.assets.Count();

            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";
            ViewData["QtyAssets"] = qty != 0 ? qty.ToString() : "0";
            //ViewData["QtyAssets"] = license.Item3.Count() != 0 ? license.Item3.Count().ToString() : "0";
            //ViewData["ListAssets"] = new List<Asset>(license.Item2.assets);

            return View(license.Item2);
        }

        // POST: License/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: List unused Licenses to add on Asset
        public IActionResult ListUnusedLLicensesToAddOnAsset()
        {
            List<License> license = service.GetAllUnusedLicenses();
            return View(license);
        }

        private bool LicenseExists(long id)
        {
            return service.LicenseExists(id);
        }

        #region DownloadFilePDF

        public IActionResult DownloadFile(long licenseID)
        {
            // Will send the productID to service & repository, where it will get the path where the file is
            // from the database. In the service will get the pdf file and convert to FileStream and send back.

            Tuple<string, FileStream, bool> stream = service.GetLicenseColPdf(licenseID);

            if (stream.Item3 == false || stream.Item2 == null)
            {
                //return NotFound();

                return new NotFoundResult();
            }

            // Will send the PDF stream to the view, changing the name!!! (security reasons)
            // In this way no path is send to or from the view!
            return new FileStreamResult(stream.Item2, "application/pdf");
        }

        #endregion
    }
}
