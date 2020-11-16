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
    public class LicenseTypeController : Controller
    {
        private readonly ILicenseTypeService service;

        public LicenseTypeController(ILicenseTypeService _service)
        {
            service = _service;
        }

        // GET: LicenseType
        public IActionResult Index()
        {
            List<LicenseType> licenseTypes = service.GetAllLicenseTypes();
            return View(licenseTypes);
        }

        // GET: LicenseType/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LicenseType licenseType = service.FindById(id.Value);
            if (licenseType == null)
            {
                return NotFound();
            }

            return View(licenseType);
        }

        // GET: LicenseType/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LicenseType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("LicenseTypeID,Name,LimitedUse,UnlimitedUse")] LicenseType licenseType)
        {
            if (ModelState.IsValid)
            {
                service.Add(licenseType);
                return RedirectToAction(nameof(Index));
            }
            return View(licenseType);
        }

        // GET: LicenseType/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LicenseType licenseType = service.FindById(id.Value);
            if (licenseType == null)
            {
                return NotFound();
            }
            return View(licenseType);
        }

        // POST: LicenseType/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("LicenseTypeID,Name,LimitedUse,UnlimitedUse")] LicenseType licenseType)
        {
            if (id != licenseType.LicenseTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(licenseType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseTypeExists(licenseType.LicenseTypeID))
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
            return View(licenseType);
        }

        // GET: LicenseType/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //LicenseType licenseType = service.FindById(id.Value);

            Tuple<long, LicenseType, List<License>> licenseType = service.GetAllLicenseTypesWithLicenses(id.Value);

            if (licenseType == null)
            {
                return NotFound();
            }

            int qtyLicense = licenseType.Item3.Count();

            int qty = qtyLicense;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyLicense"] = qtyLicense != 0 ? qtyLicense.ToString() : "0";
            ViewData["ListLicenses"] = new List<License>(licenseType.Item3);

            return View(licenseType.Item2);
        }

        // POST: LicenseType/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseTypeExists(long id)
        {
            return service.LicenseTypeExists(id);
        }
    }
}
