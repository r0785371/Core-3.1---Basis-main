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
    public class AssetLicenseController : Controller
    {
        private readonly IAssetLicenseService service;

        public AssetLicenseController(IAssetLicenseService _service)
        {
            service = _service;
        }

        // GET: AssetLicense
        public IActionResult Index()
        {
            List<AssetLicense> assetLicense = service.GetAllAssetLicenses();
            return View(assetLicense);
        }

        // GET: AssetLicense/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetLicense assetLicense = service.FindById(id.Value);
            if (assetLicense == null)
            {
                return NotFound();
            }

            return View(assetLicense);
        }

        // GET: AssetLicense/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseID"] = new List<SelectListItem>(service.GetSelectListLicenses());
            return View();
        }

        // POST: AssetLicense/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("AssetLicenseID,AssetID,LicenseID")] AssetLicense assetLicense)
        {
            if (ModelState.IsValid)
            {
                service.Add(assetLicense);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseID"] = new List<SelectListItem>(service.GetSelectListLicenses());
            return View(assetLicense);
        }

        // GET: AssetLicense/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetLicense assetLicense = service.FindById(id.Value);
            if (assetLicense == null)
            {
                return NotFound();
            }
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseID"] = new List<SelectListItem>(service.GetSelectListLicenses());
            return View(assetLicense);
        }

        // POST: AssetLicense/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("AssetLicenseID,AssetID,LicenseID")] AssetLicense assetLicense)
        {
            if (id != assetLicense.AssetLicenseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(assetLicense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetLicenseExists(assetLicense.AssetLicenseID))
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
            ViewData["AssetID"] = new List<SelectListItem>(service.GetSelectListAssets());
            ViewData["LicenseID"] = new List<SelectListItem>(service.GetSelectListLicenses());
            return View(assetLicense);
        }

        // GET: AssetLicense/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id, long? assetID)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction("Edit", "Asset", new { id = assetID });
            }

            AssetLicense assetLicense = service.FindById(id.Value);
            if (assetLicense == null)
            {
                //return NotFound();
                return RedirectToAction("Edit", "Asset", new { id = assetID });
            }

            ViewData["AssetID"] = assetID;

            return View(assetLicense);
        }

        // POST: AssetLicense/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id, long assetID)
        {
            service.Remove(id);

            //if (assetID.HasValue)
            //{
            //    return RedirectToAction("Edit", "Asset", new { id = assetID });
            //}
            //return RedirectToAction(nameof(Index));

            return RedirectToAction("Edit", "Asset", new { id = assetID });
        }

        private bool AssetLicenseExists(long id)
        {
            return service.AssetLicenseExists(id);
        }
    }
}
