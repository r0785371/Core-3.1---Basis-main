using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AssetBeheerPortOfAntwerp.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService service;

        public SupplierController(ISupplierService _service)
        {
            service = _service;
        }


        // GET: Supplier
        public IActionResult Index()
        {
            List<Supplier> suppliers = service.GetAllSuppliers();
            return View(suppliers);
        }

        // GET: Supplier/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier supplier = service.FindById(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("SupplierID,Name,Street,Number,Zip code,City,State,Country,ServiceDeskTel,ServiceDeskMail,ServiceDeskWeb,IsActief,HasHardware,HasSoftware")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                service.Add(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier supplier = service.FindById(id.Value);

            //Tuple<long, Supplier, List<Hardware>, List<Software>, List<Purchase>> supplier = service.GetSupplierWithSub(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            //ViewData["ListHardware"] = new List<Hardware>(supplier.Item3);
            //ViewData["ListSoftware"] = new List<Software>(supplier.Item4);
            //ViewData["ListPurchase"] = new List<Purchase>(supplier.Item5);

            //return View(supplier.Item2);

            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("SupplierID,Name,Street,Number,Zip code,City,State,Country,ServiceDeskTel,ServiceDeskMail,ServiceDeskWeb,IsActief,HasHardware,HasSoftware")] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        //// GET: Supplier/Delete/
        //[Authorize(Roles = "Administrator,UserCRUD")]
        //public IActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //Tuple<long, Supplier, int, int> supplier = service.CanDeleteSupplier(id.Value);

        //    Tuple<long, Supplier, List<Hardware>, List<Software>, List<Purchase>> supplier = service.GetSupplierWithSub(id.Value);

        //    if (supplier.Item2 == null)
        //    {
        //        return RedirectToAction("Edit", new { id });
        //    }

        //    int qtyHardware = supplier.Item3.Count();
        //    int qtySoftware = supplier.Item4.Count();
        //    int qtyPurchase = supplier.Item5.Count();

        //    int qty = qtyHardware + qtySoftware + qtyPurchase;

        //    ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

        //    ViewData["QtyHardware"] = qtyHardware != 0 ? qtyHardware.ToString() : "0";
        //    ViewData["ListHardware"] = new List<Hardware>(supplier.Item3);

        //    ViewData["QtySoftware"] = qtySoftware != 0 ? qtySoftware.ToString() : "0";
        //    ViewData["ListSoftware"] = new List<Software>(supplier.Item4);

        //    ViewData["QtyPurchase"] = qtyPurchase != 0 ? qtyPurchase.ToString() : "0";
        //    ViewData["ListPurchase"] = new List<Purchase>(supplier.Item5);

        //    return View(supplier.Item2);
        //}

        // POST: Supplier/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(long id)
        {
            return service.SupplierExists(id);
        }
    }
}
