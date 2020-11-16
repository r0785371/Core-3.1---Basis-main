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
    public class BackupController : Controller
    {
        private readonly IBackupService service;

        public BackupController(IBackupService _service)
        {
            service = _service;
        }

        // GET: Backup
        public IActionResult Index()
        {
            List<Backup> backups = service.GetAllBackups();
            return View(backups);
        }

        // GET: Backup/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Backup backup = service.FindById(id.Value);
            if (backup == null)
            {
                return NotFound();
            }

            return View(backup);
        }

        // GET: Backup/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long assetID)
        {
            ViewData["AssetID"] = assetID;
            ViewData["BackupTypeId"] = new List<SelectListItem>(service.GetSelectListBackupTypes());
            ViewData["PersonId"] = new List<SelectListItem>(service.GetSelectListPeople());
            return View();
        }

        // POST: Backup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long assetID, [Bind("Notes,BackupDate,PersonId,BackupTypeId,AssetId")] Backup backup)
        {
            backup.AssetId = assetID;

            if (ModelState.IsValid)
            {
                service.Add(backup);
                return RedirectToAction("Edit", "Asset", new { id = assetID });
            }
            ViewData["AssetID"] = assetID;
            ViewData["BackupTypeId"] = new List<SelectListItem>(service.GetSelectListBackupTypes());
            ViewData["PersonId"] = new List<SelectListItem>(service.GetSelectListPeople());
            return View(backup);
        }

        // GET: Backup/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Backup backup = service.FindById(id.Value);
            if (backup == null)
            {
                return NotFound();
            }
            ViewData["BackupTypeId"] = new List<SelectListItem>(service.GetSelectListBackupTypes());
            ViewData["PersonId"] = new List<SelectListItem>(service.GetSelectListPeople());
            return View(backup);
        }

        // POST: Backup/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("BackupID,Notes,BackupDate,PersonId,BackupTypeId,AssetId")] Backup backup)
        {
            if (id != backup.BackupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(backup);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackupExists(backup.BackupID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Asset", new { id = backup.AssetId });
            }
            ViewData["BackupTypeId"] = new List<SelectListItem>(service.GetSelectListBackupTypes());
            ViewData["PersonId"] = new List<SelectListItem>(service.GetSelectListPeople());
            return View(backup);
        }

        // GET: Backup/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Backup backup = service.FindById(id.Value);
            if (backup == null)
            {
                return NotFound();
            }

            return View(backup);
        }

        // POST: Backup/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult BackupNeededChart()
        {
            

            return View();
        }


        private bool BackupExists(long id)
        {
            return service.BackupExists(id);
        }
    }
}
