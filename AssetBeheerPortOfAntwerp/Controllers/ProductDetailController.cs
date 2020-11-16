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
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService service;

        public ProductDetailController(IProductDetailService _service)
        {
            service = _service;
        }

        // GET: ProductDetail
        public IActionResult Index()
        {
            List<ProductDetail> productDetails = service.GetAllProductDetails();
            return View(productDetails);
        }

        // GET: ProductDetail/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductDetail productDetail = service.FindById(id.Value);
            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }

        // GET: ProductDetail/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long productID, string productChild)
        {
            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(productID));
            ViewData["ProductID"] = productID;
            ViewData["productChild"] = productChild;

            return View();
        }

        // POST: ProductDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(string productChild, [Bind("ProductDetailID,ProductID,DetailID,Definition1,Definition2")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                service.Add(productDetail);

                //When product is succesful added, it will return to the Software / Hardware controller.
                if (productChild == "Software")
                {
                    return RedirectToAction("Edit", "Software", new { id = productDetail.ProductID });
                }
                else
                {
                    return RedirectToAction("Edit", "Hardware", new { id = productDetail.ProductID });
                }
            }

            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(productDetail.ProductID));
            ViewData["ProductID"] = productDetail.ProductID;

            return View(productDetail);
        }

        // GET: ProductDetail/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductDetail productDetail = service.FindById(id.Value);
            if (productDetail == null)
            {
                return NotFound();
            }

            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(id.Value));
            ViewData["ProductID"] = productDetail.ProductID;

            return View(productDetail);
        }

        // POST: ProductDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, long productID, string productChild, [Bind("ProductDetailID,ProductID,DetailID,Definition1,Definition2")] ProductDetail productDetail)
        {
            if (id != productDetail.ProductDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(productDetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDetailExists(productDetail.ProductDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //When product is succesful added, it will return to the Software / Hardware controller.
                if (productChild == "Software")
                {
                    return RedirectToAction("Edit", "Software", new { id = productDetail.ProductID });
                }
                else
                {
                    return RedirectToAction("Edit", "Hardware", new { id = productDetail.ProductID });
                }
            }
            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(productDetail.ProductDetailID));
            ViewData["ProductID"] = productDetail.ProductID;
            return View(productDetail);
        }

        // GET: ProductDetail/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductDetail productDetail = service.FindById(id.Value);
            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }

        // POST: ProductDetail/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id, string productChild)
        {
            ProductDetail productDetail = service.FindById(id);
            service.Remove(id);
            
            //When product is succesful added, it will return to the Software / Hardware controller.
            if (productChild == "Software")
            {
                return RedirectToAction("Edit", "Software", new { id = productDetail.ProductID });
            }
            else
            {
                return RedirectToAction("Edit", "Hardware", new { id = productDetail.ProductID });
            }
        }

        private bool ProductDetailExists(long id)
        {
            return service.ProductDetailExists(id);
        }
    }
}
