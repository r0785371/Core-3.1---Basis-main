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
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class PurchaseItemController : Controller
    {
        private readonly IPurchaseItemService service;

        public PurchaseItemController(IPurchaseItemService _service)
        {
            service = _service;
        }

        // GET: PurchaseItem
        public IActionResult Index()
        {
            List<PurchaseItem> purchaseItems = service.GetAllPurchaseItems();
            return View(purchaseItems);
        }

        // GET: PurchaseItem/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PurchaseItem purchaseItem = service.FindById(id.Value);
            if (purchaseItem == null)
            {
                return NotFound();
            }

            return View(purchaseItem);
        }

        // GET: PurchaseItem/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long purchaseID, bool? hasHardware, bool? hasSoftware)
        {

            
            ViewData["PurchaseID"] = purchaseID;
            //List without Products that have NoSupport and are OUT
            //ViewData["ProductID"] = new List<SelectListItem>(service.GetSelectListActiveProducts());

            ViewData["ProductID"] = new List<SelectListItem>(service.GetSelectListProductsOfSupplier(purchaseID, hasHardware, hasSoftware));
            
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusPurchase());
            
            return View();
        }

        // POST: PurchaseItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("PurchaseItemID,PurchaseID,ProductID,StatusID,PurchaseQty,Price,WarentyIntervalMonth,DeliveryDate,AssetID,LicenseID,HasAssetOrLicense")] PurchaseItem purchaseItem)
        {
            if (ModelState.IsValid)
            {
                Tuple<string, int> add = service.Add(purchaseItem);

                if (add.Item1 == "Hardware")
                {
                    //return RedirectToAction("CreatAutomatic", "Asset", new { purchaseItemID = purchaseItem.PurchaseItemID, qtyAdd = add.Item2 });
                    return RedirectToAction("CreateTagNumber", "Asset", new { purchaseItemID = purchaseItem.PurchaseItemID });
                }

                if (add.Item1 == "Software")
                {
                    return RedirectToAction("CreatAutomatic", "License", new { purchaseItemID = purchaseItem.PurchaseItemID, qtyAdd = add.Item2 });
                }

                return RedirectToAction("Edit", "Purchase", new { id = purchaseItem.PurchaseID });
            }
            ViewData["ProductID"] = new List<SelectListItem>(service.GetSelectListProducts());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusPurchase());
            return View(purchaseItem);
        }

        // GET: PurchaseItem/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id, long? assetID, long? licenseID)
        {
            if (id == null)
            {
                return NotFound();
            }

            //PurchaseItem purchaseItem = service.FindById(id.Value);

            Tuple<long, PurchaseItem, List<Asset>, List<License>> purchaseItem = service.GetPurchaseItemWithSub(id.Value);

            if (purchaseItem == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new List<SelectListItem>(service.GetSelectListProducts());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusPurchase());
            ViewData["ProductChild"] = purchaseItem.Item2.Product.ProductType.ProductChild;
            
            ViewData["ListAssets"] = purchaseItem.Item3;
            ViewData["ListLicense"] = purchaseItem.Item4;

            ViewData["AssetID"] = assetID;
            ViewData["LicenseID"] = licenseID;

            return View(purchaseItem.Item2);
        }

        // POST: PurchaseItem/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, long purchaseID, [Bind("PurchaseItemID,PurchaseID,ProductID,StatusID,PurchaseQty,Price,WarentyIntervalMonth,DeliveryDate,AssetID,LicenseID,HasAssetOrLicense")] PurchaseItem purchaseItem)
        {
            if (id != purchaseItem.PurchaseItemID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    Tuple<string, int> add = service.Update(purchaseItem);

                    if (add.Item1 == "Hardware")
                    {
                        //return RedirectToAction("CreatAutomatic", "Asset", new { purchaseItemID = purchaseItem.PurchaseItemID, qtyAdd = add.Item2 });
                        return RedirectToAction("CreateTagNumber", "Asset", new { purchaseItemID = purchaseItem.PurchaseItemID });
                    }

                    if (add.Item1 == "Software")
                    {
                        return RedirectToAction("CreatAutomatic", "License", new { purchaseItemID = purchaseItem.PurchaseItemID, qtyAdd = add.Item2 });
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseItemExists(purchaseItem.PurchaseItemID))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }

                // When no need to add Asset / License, than go back to the Purchase
                return RedirectToAction("Edit", "Purchase", new { id = purchaseItem.PurchaseID });

                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("Edit", "Purchase", new { id = purchaseID });
            }
            ViewData["ProductID"] = new List<SelectListItem>(service.GetSelectListProducts());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusPurchase());

            return View(purchaseItem);
        }

        // GET: PurchaseItem/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tuple<long, PurchaseItem, List<Asset>, List<License>> purchaseItem = service.GetPurchaseItemWithSub(id.Value);

            if (purchaseItem.Item2 == null)
            {
                return RedirectToAction("Edit", new { id });
            }

            ViewData["ListAssets"] = purchaseItem.Item3;
            ViewData["ListLicense"] = purchaseItem.Item4;

            return View(purchaseItem.Item2);
        }

        // POST: PurchaseItem/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            PurchaseItem purchaseItem = service.FindById(id);
            service.Remove(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Edit", "Purchase", new { id = purchaseItem.PurchaseID });
        }

        private bool PurchaseItemExists(long id)
        {
            return service.PurchaseItemExists(id);
        }


        public IActionResult ListToOrderItems()
        {
            List<PurchaseItem> toOrderItems = service.GetListPurchaseItemsToOrder();
            return View(toOrderItems);
        }

        public IActionResult ListOrderedItems()
        {
            List<PurchaseItem> orderedItems = service.GetListPurchaseItemsOrdered();
            return View(orderedItems);
        }

        //public JsonResult OnGetPurchaseItemPivotData()
        //{
        //    List<PurchaseItem> purchaseItems = service.GetAllPurchaseItems();

        //    //ViewData["AssetPivotData"] = assets;

        //    return new JsonResult(purchaseItems);
        //}
    }
}
