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
    public class SoftwareTypeController : Controller
    {
        private readonly ISoftwareTypeService service;

        public SoftwareTypeController(ISoftwareTypeService _service)
        {
            service = _service;
        }

        // GET: SoftwareType
        public IActionResult Index()
        {
            List<SoftwareType> softwareTypes = service.GetAllSoftwareTypes();
            return View(softwareTypes);
        }

        // GET: SoftwareType/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SoftwareType softwareType = service.FindById(id.Value);
            if (softwareType == null)
            {
                return NotFound();
            }

            return View(softwareType);
        }

        // GET: SoftwareType/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SoftwareType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("SoftwareTypeID,Name")] SoftwareType softwareType)
        {
            if (ModelState.IsValid)
            {
                service.Add(softwareType);
                return RedirectToAction(nameof(Index));
            }
            return View(softwareType);
        }

        // GET: SoftwareType/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SoftwareType softwareType = service.FindById(id.Value);
            if (softwareType == null)
            {
                return NotFound();
            }
            return View(softwareType);
        }

        // POST: SoftwareType/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("SoftwareTypeID,Name")] SoftwareType softwareType)
        {
            if (id != softwareType.SoftwareTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(softwareType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareTypeExists(softwareType.SoftwareTypeID))
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
            return View(softwareType);
        }

        // GET: SoftwareType/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //SoftwareType softwareType = service.FindById(id.Value);

            Tuple<long, SoftwareType, List<Software>> softwareType = service.GetAllSoftwareTypesWithSoftware(id.Value);

            if (softwareType == null)
            {
                return NotFound();
            }

            int qtySoftware = softwareType.Item3.Count();

            int qty = qtySoftware;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtySoftware"] = qtySoftware != 0 ? qtySoftware.ToString() : "0";
            ViewData["ListSoftware"] = new List<Software>(softwareType.Item3);

            return View(softwareType.Item2);
        }

        // POST: SoftwareType/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareTypeExists(long id)
        {
            return service.SoftwareTypeExists(id);
        }
    }
}
