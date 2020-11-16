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
    public class LicenseValidityTypeController : Controller
    {
        private readonly ILicenseValidityTypeService service;

        public LicenseValidityTypeController(ILicenseValidityTypeService _service)
        {
            service = _service;
        }

        // GET: LicenseValidityType
        public IActionResult Index()
        {
            List<LicenseValidityType> licenseValidityTypes = service.GetAllLicenseValidityTypes();
            return View(licenseValidityTypes);
        }

        // GET: LicenseValidityType/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LicenseValidityType licenseValidityType = service.FindById(id.Value);
            if (licenseValidityType == null)
            {
                return NotFound();
            }

            return View(licenseValidityType);
        }

        // GET: LicenseValidityType/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LicenseValidityType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("LicenseValidityTypeID,Name")] LicenseValidityType licenseValidityType)
        {
            if (ModelState.IsValid)
            {
                service.Add(licenseValidityType);
                return RedirectToAction(nameof(Index));
            }
            return View(licenseValidityType);
        }

        // GET: LicenseValidityType/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LicenseValidityType licenseValidityType = service.FindById(id.Value);
            if (licenseValidityType == null)
            {
                return NotFound();
            }
            return View(licenseValidityType);
        }

        // POST: LicenseValidityType/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("LicenseValidityTypeID,Name")] LicenseValidityType licenseValidityType)
        {
            if (id != licenseValidityType.LicenseValidityTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(licenseValidityType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseValidityTypeExists(licenseValidityType.LicenseValidityTypeID))
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
            return View(licenseValidityType);
        }

        // GET: LicenseValidityType/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //LicenseValidityType licenseValidityType = service.FindById(id.Value);

            Tuple<long, LicenseValidityType, List<License>> licenseValidityType = service.GetAllLicenseValidityTypesWithLicenses(id.Value);

            if (licenseValidityType == null)
            {
                return NotFound();
            }

            int qtyLicense = licenseValidityType.Item3.Count();

            int qty = qtyLicense;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyLicense"] = qtyLicense != 0 ? qtyLicense.ToString() : "0";
            ViewData["ListLicenses"] = new List<License>(licenseValidityType.Item3);

            return View(licenseValidityType.Item2);
        }

        // POST: LicenseValidityType/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseValidityTypeExists(long id)
        {
            return service.LicenseValidityTypeExists(id);
        }
    }
}
