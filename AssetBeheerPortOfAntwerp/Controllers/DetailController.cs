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
    public class DetailController : Controller
    {
        private readonly IDetailService service;

        public DetailController(IDetailService _service)
        {
            service = _service;
        }

        // GET: Detail
        public IActionResult Index()
        {
            List<Detail> details = service.GetAllDetails();
            return View(details);
        }

        // GET: Detail/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Detail detail = service.FindById(id.Value);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: Detail/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["DetailMainID"] = new List<SelectListItem>(service.GetSelectListDetailMains());
            ViewData["DetailSubID"] = new List<SelectListItem>(service.GetSelectListDetailSubs());
            ViewData["SelectedProductTypes"] = new List<SelectListItem>(service.GetSelectListProductTypes());
            return View();
        }

        // POST: Detail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("DetailID,DetailMainID,DetailSubID,SelectProductTypes")] Detail detail)
        {
            if (ModelState.IsValid)
            {
                service.Add(detail);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DetailMainID"] = new List<SelectListItem>(service.GetSelectListDetailMains());
            ViewData["DetailSubID"] = new List<SelectListItem>(service.GetSelectListDetailSubs());
            ViewData["SelectedProductTypes"] = new List<SelectListItem>(service.GetSelectListProductTypes());
            return View(detail);
        }

        // GET: Detail/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Detail detail = service.FindById(id.Value);
            if (detail == null)
            {
                return NotFound();
            }
            ViewData["DetailMainID"] = new List<SelectListItem>(service.GetSelectListDetailMains());
            ViewData["DetailSubID"] = new List<SelectListItem>(service.GetSelectListDetailSubs());
            ViewData["SelectedProductTypes"] = new List<SelectListItem>(service.GetSelectListProductTypes());
            return View(detail);
        }

        // POST: Detail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("DetailID,DetailMainID,DetailSubID,SelectProductTypes")] Detail detail)
        {
            if (id != detail.DetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(detail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailExists(detail.DetailID))
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
            ViewData["DetailMainID"] = new List<SelectListItem>(service.GetSelectListDetailMains());
            ViewData["DetailSubID"] = new List<SelectListItem>(service.GetSelectListDetailSubs());
            ViewData["SelectedProductTypes"] = new List<SelectListItem>(service.GetSelectListProductTypes());
            return View(detail);
        }

        // GET: Detail/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Detail detail = service.FindById(id.Value);

            Tuple<long, Detail, List<ProductDetail>, List<AssetDetail>> detail = service.GetAllDetailsWithSub(id.Value);

            int qtyProductDetail = detail.Item3.Count();
            int qtyAssetDetail = detail.Item4.Count();

            int qty = qtyProductDetail + qtyAssetDetail;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyProductDetail"] = qtyProductDetail != 0 ? qtyProductDetail.ToString() : "0";
            ViewData["ListProductDetails"] = new List<ProductDetail>(detail.Item3);

            ViewData["QtyAssetDetail"] = qtyAssetDetail != 0 ? qtyAssetDetail.ToString() : "0";
            ViewData["ListAssetDetails"] = new List<AssetDetail>(detail.Item4);


            if (detail == null)
            {
                return NotFound();
            }

            return View(detail.Item2);
        }

        // POST: Detail/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DetailExists(long id)
        {
            return service.DetailExists(id);
        }
    }
}
