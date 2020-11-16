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
    public class AssetDetailController : Controller
    {
        private readonly IAssetDetailService service;

        public AssetDetailController(IAssetDetailService _service)
        {
            service = _service;
        }

        // GET: AssetDetail
        public IActionResult Index()
        {
            List<AssetDetail> assetDetails = service.GetAllAssetDetails();
            return View(assetDetails);
        }

        // GET: AssetDetail/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetDetail assetDetail = service.FindById(id.Value);
            if (assetDetail == null)
            {
                return NotFound();
            }

            return View(assetDetail);
        }

        // GET: AssetDetail/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long assetID)
        {
            ViewData["AssetID"] = assetID;
            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(assetID));
            return View();
        }

        // POST: AssetDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long assetID, [Bind("AssetDetailID,AssetID,DetailID,Definition1,Definition2")] AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                service.Add(assetDetail);
                return RedirectToAction("Edit", "Asset", new { id = assetID });
            }
            ViewData["AssetID"] = assetID;
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            return View(assetDetail);
        }

        // GET: AssetDetail/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetDetail assetDetail = service.FindById(id.Value);
            if (assetDetail == null)
            {
                return NotFound();
            }
            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(id.Value));
            return View(assetDetail);
        }

        // POST: AssetDetail/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("AssetDetailID,AssetID,DetailID,Definition1,Definition2")] AssetDetail assetDetail)
        {
            if (id != assetDetail.AssetDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(assetDetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetDetailExists(assetDetail.AssetDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Asset", new { id = assetDetail.AssetID });
            }
            //ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetails());
            ViewData["DetailID"] = new List<SelectListItem>(service.GetSelectListDetailsOfProductType(assetDetail.AssetID));
            return View(assetDetail);
        }

        // GET: AssetDetail/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetDetail assetDetail = service.FindById(id.Value);
            if (assetDetail == null)
            {
                return NotFound();
            }

            return View(assetDetail);
        }

        // POST: AssetDetail/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            AssetDetail assetDetail = service.FindById(id);
            service.Remove(id);
            return RedirectToAction("Edit", "Asset", new { id = assetDetail.AssetID });
        }

        private bool AssetDetailExists(long id)
        {
            return service.AssetDetailExists(id);
        }
    }
}
