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
    public class RackLocationController : Controller
    {
        private readonly IRackLocationService service;

        public RackLocationController(IRackLocationService _service)
        {
            service = _service;
        }

        // GET: RackLocation
        public IActionResult Index()
        {
            List<RackLocation> rackLocations = service.GetAllRackLocations();
            return View(rackLocations);
        }

        // GET: RackLocation/Details
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RackLocation rackLocation = service.FindById(id.Value);

            if (rackLocation == null)
            {
                return NotFound();
            }

            return View(rackLocation);
        }

        // GET: RackLocation/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocation());
            ViewData["RackID"] = new List<SelectListItem>(service.GetSelectListRack());
            return View();
        }

        // POST: RackLocation/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("RackLocationID,LocationID,RackID,RackNo")] RackLocation rackLocation)
        {
            if (ModelState.IsValid)
            {
                service.Add(rackLocation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocation());
            ViewData["RackID"] = new List<SelectListItem>(service.GetSelectListRack());
            return View(rackLocation);
        }

        // GET: RackLocation/Edit
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RackLocation rackLocation = service.FindById(id.Value);
            if (rackLocation == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocation());
            ViewData["RackID"] = new List<SelectListItem>(service.GetSelectListRack());
            return View(rackLocation);
        }

        // POST: RackLocation/Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("RackLocationID,LocationID,RackID,RackNo")] RackLocation rackLocation)
        {
            if (id != rackLocation.RackLocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(rackLocation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RackLocationExists(rackLocation.RackLocationID))
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
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocation());
            ViewData["RackID"] = new List<SelectListItem>(service.GetSelectListRack());
            return View(rackLocation);
        }

        // GET: RackLocation/Delete
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tuple<long, RackLocation, List<Asset>> rackLocation = service.GetAllRackLocationsWithAssets(id.Value);

            if (rackLocation == null)
            {
                return NotFound();
            }

            int qtyAsset = rackLocation.Item3.Count();

            ViewData["Qty"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(rackLocation.Item3);

            return View(rackLocation.Item2);
        }

        // POST: RackLocation/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RackLocationExists(long id)
        {
            return service.RackLocationExists(id);
        }
    }
}
