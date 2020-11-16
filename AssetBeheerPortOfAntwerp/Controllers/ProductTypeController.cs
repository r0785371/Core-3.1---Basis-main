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
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService service;

        public ProductTypeController(IProductTypeService _service)
        {
            service = _service;
        }

        
        // GET: ProductType
        public IActionResult Index()
        {
            List<ProductType> productTypes = service.GetAllProductTypes();
            return View(productTypes);
        }

        // GET: ProductType/Details
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductType productType = service.FindById(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductType/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["ProductTypeGroupID"] = new List<SelectListItem>(service.GetSelectListProductTypeGroups());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());
            return View();
        }

        // POST: ProductType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("ProductTypeID,ProductChild, Ref,Name,Description,IsGroup,ProductTypeGroupID,IsProduct,IsLicense,IsAsset,ExpirePeriodMonth,WarningPeriodID,TagNo,IsComponent,HasBackup,BackupInterval,GoInRack")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                service.Add(productType);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeGroupID"] = new List<SelectListItem>(service.GetSelectListProductTypeGroups());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());
            return View(productType);
        }

        // GET: ProductType/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductType productType = service.FindById(id.Value);
            if (productType == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeGroupID"] = new List<SelectListItem>(service.GetSelectListProductTypeGroups());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());
            return View(productType);
        }

        // POST: ProductType/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("ProductTypeID,ProductChild, Ref,Name,Description,IsGroup,ProductTypeGroupID,IsProduct,IsLicense,IsAsset,ExpirePeriodMonth,WarningPeriodID,TagNo,IsComponent,HasBackup,BackupInterval,GoInRack")] ProductType productType)
        {
            if (id != productType.ProductTypeID)
            {
                return NotFound();
            }

            // Need to clear and add ModelState new as if user set selectbox to null!
            ModelState.Clear();
            TryValidateModel(productType);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(productType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeID))
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
            ViewData["ProductTypeGroupID"] = new List<SelectListItem>(service.GetSelectListProductTypeGroups());
            ViewData["WarningPeriodID"] = new List<SelectListItem>(service.GetSelectListWarningPeriods());
            return View(productType);
        }

        // GET: ProductType/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //ProductType productType = service.FindById(id.Value);

            Tuple<long, ProductType, List<Product>> productType = service.GetProductTypeWithProducts(id.Value);

            if (productType.Item2 == null)
            {
                return NotFound();
            }

            int qty = productType.Item3.Count();

            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";
            ViewData["ListProducts"] = new List<Product>(productType.Item3);

            return View(productType.Item2);
        }

        // POST: ProductType/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(long id)
        {
            return service.ProductTypeExists(id);
        }
    }
}
