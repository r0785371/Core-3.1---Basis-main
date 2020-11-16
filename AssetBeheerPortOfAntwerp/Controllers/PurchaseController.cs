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
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService service;

        public PurchaseController(IPurchaseService _service)
        {
            service = _service;
        }

        // GET: Purchase
        public IActionResult Index()
        {
            List<Purchase> purchases = service.GetAllPurchases();
            return View(purchases);
        }

        // GET: Purchase/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Purchase purchase = service.FindById(id.Value);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchase/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            //Need to inicialize to be able to send list of PurchaseTypes 
            Purchase purchase = new Purchase();
            purchase.ListPurchaseTypes = new List<PurchaseType>(service.GetAllPurchaseTypes());

            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSelectListSuppliers());
            return View(purchase);
        }

        // POST: Purchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long selectedPurchaseTypeID, [Bind("PurchaseID,PurchaseTypeID,SupplierID,No,Date")] Purchase purchase)
        {

            if (ModelState.IsValid)
            {
                purchase = service.Add(purchase);
                return RedirectToAction("Edit", new { id = purchase.PurchaseID });
            }
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSelectListSuppliers());
            return View(purchase);

        }

        // GET: Purchase/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            purchaseViewModel purchase = service.LoadPurchaseSubFormPurchaseItems(id.Value);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSelectListSuppliers());
            return View(purchase);
        }

        // POST: Purchase/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, bool? goToPurchaseItem, bool? hasHardware, bool? hasSoftware, purchaseViewModel purchaseViewModel, [Bind("PurchaseID,Type,IsPurchase,IsProcurement,SupplierID,No,Date")] Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            //Data comes back from the view via purchaseViewModel but need to be saved to purchase,
            
            //so, a) we need to actualised the PurchaseTypeID in purchase and ...
            purchase.PurchaseTypeID = purchaseViewModel.SelectedPurchaseTypeID;

            
            // ... b) we need to switch the ModelState.
            ModelState.Clear();
            TryValidateModel(purchase);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(purchase);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (goToPurchaseItem == true)
                {
                    return RedirectToAction("Create", "PurchaseItem", new { purchaseID = purchase.PurchaseID, hasHardware, hasSoftware });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSelectListSuppliers());
            return View(purchase);
        }

        // GET: Purchase/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int QtyPurchaseItems = 0;
            QtyPurchaseItems = service.QtyPurchaseItemsPerPurchase(id.Value);
            
            if (QtyPurchaseItems > 0)
            {
                ViewData["QtyPurchaseItems"] = QtyPurchaseItems.ToString();
            }

            Purchase purchase = service.FindById(id.Value);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchase/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(long id)
        {
            return service.PurchaseExists(id);
        }
    }
}
