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
    public class ProductSupplierController : Controller
    {
        private readonly IProductSupplierService service;

        public ProductSupplierController(IProductSupplierService _service)
        {
            service = _service;
        }

        //Has no Index as ProductSuppliers is only accessible from Software / Hardware
        // GET: ProductSupplier
        //publicIActionResult Index()
        //{
        //    
        //    return View();
        //}

        // GET: ProductSupplier/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductSupplier productSupplier = service.FindById(id.Value);
            if (productSupplier == null)
            {
                return NotFound();
            }

            return View(productSupplier);
        }

        // GET: ProductSupplier/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long productID, string productChild)
        {
            //Will get only the suppliers of Hardware or Software
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSuppliersOfProductChild(productChild));
            ViewData["ProductID"] = productID;
            ViewData["productChild"] = productChild;
            return View();
        }

        // POST: ProductSupplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(string productChild, [Bind("ProductSupplierID,SupplierID,ProductID,RefSupplier,ProductNameSupplier,Price")] ProductSupplier productSupplier)
        {
            if (ModelState.IsValid)
            {
                service.Add(productSupplier);

                //When product is succesful added, it will return to the Software / Hardware controller.
                if (productChild == "Software")
                {
                    return RedirectToAction("Edit", "Software", new { id = productSupplier.ProductID });
                }
                else
                {
                    return RedirectToAction("Edit", "Hardware", new { id = productSupplier.ProductID });
                }

            }

            ViewData["ProductID"] = productSupplier.ProductID;
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSuppliersOfProductChild(productChild));

            return View(productSupplier);
        }

        // GET: ProductSupplier/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductSupplier productSupplier = service.FindById(id.Value);
            if (productSupplier == null)
            {
                return NotFound();
            }
            
            string productChild = productSupplier.Product.ProductType.ProductChild.ToString();
            
            ViewData["ProductID"] = productSupplier.ProductID;
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSuppliersOfProductChild(productChild));

            return View(productSupplier);
        }

        // POST: ProductSupplier/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, long productID, string productChild, [Bind("ProductSupplierID,SupplierID,ProductID,RefSupplier,ProductNameSupplier,Price")] ProductSupplier productSupplier)
        {
            if (id != productSupplier.ProductSupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(productSupplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSupplierExists(productSupplier.ProductSupplierID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                                
                //When product is succesful updated it will return to the Software / to Hardware controller.
                if (productChild == "Software")
                {
                    return RedirectToAction("Edit", "Software", new { id = productID });
                }
                else
                {
                    return RedirectToAction("Edit", "Hardware", new { id = productID });
                }
            }
            
            ViewData["SupplierID"] = new List<SelectListItem>(service.GetSelectListAllSuppliers());
            return View(productSupplier);
        }

        // GET: ProductSupplier/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductSupplier productSupplier = service.FindById(id.Value);
            if (productSupplier == null)
            {
                return NotFound();
            }

            return View(productSupplier);
        }

        // POST: ProductSupplier/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id, string productChild)
        {
            //long productID;
            ProductSupplier productSupplier = service.FindById(id);
            //productID = productSupplier.ProductID;

            service.Remove(id);

            //When product is succesful added, it will return to the Software / Hardware controller.
            if (productChild == "Software")
            {
                return RedirectToAction("Edit", "Software", new { id = productSupplier.ProductID });
            }
            else
            {
                return RedirectToAction("Edit", "Hardware", new { id = productSupplier.ProductID });
            }
        }

        private bool ProductSupplierExists(long id)
        {
            return service.ProductSupplierExists(id);
        }
    }
}
