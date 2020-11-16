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
    public class BackupTypeController : Controller
    {
        private readonly IBackupTypeService service;

        public BackupTypeController(IBackupTypeService _service)
        {
            service = _service;
        }

        // GET: BackupType
        public IActionResult Index()
        {
            List<BackupType> backupTypes = service.GetAllBackupTypes();
            return View((backupTypes));
        }

        // GET: BackupType/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BackupType backupType = service.FindById(id.Value);
            if (backupType == null)
            {
                return NotFound();
            }

            return View(backupType);
        }

        // GET: BackupType/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackupType/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("BackupTypeID,Name")] BackupType backupType)
        {
            if (ModelState.IsValid)
            {
                service.Add(backupType);
                return RedirectToAction(nameof(Index));
            }
            return View(backupType);
        }

        // GET: BackupType/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BackupType backupType = service.FindById(id.Value);
            if (backupType == null)
            {
                return NotFound();
            }
            return View(backupType);
        }

        // POST: BackupType/Edit/        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("BackupTypeID,Name")] BackupType backupType)
        {
            if (id != backupType.BackupTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(backupType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackupTypeExists(backupType.BackupTypeID))
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
            return View(backupType);
        }


        // GET: BackupType/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //BackupType backupType = service.FindById(id.Value);

            Tuple<long, BackupType, List<Backup>> backupType = service.GetAllBackupTypesWithBackups(id.Value);

            if (backupType == null)
            {
                return NotFound();
            }

            int qtyBackup = backupType.Item3.Count();

            int qty = qtyBackup;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyBackup"] = qtyBackup != 0 ? qtyBackup.ToString() : "0";
            ViewData["ListBackups"] = new List<Backup>(backupType.Item3);

            return View(backupType.Item2);
        }

        // POST: BackupType/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {

            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BackupTypeExists(long id)
        {
            return service.BackupTypeExists(id);
        }
    }

}
