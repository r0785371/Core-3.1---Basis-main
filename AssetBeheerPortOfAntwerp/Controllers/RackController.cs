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
    public class RackController : Controller
    {
        private readonly IRackService service;

        public RackController(IRackService _service)
        {
            service = _service;
        }

        // GET: Rack
        public IActionResult Index()
        {
            List<Rack> racks = service.GetAllRacks();
            return View(racks);
        }

        // GET: Rack/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rack rack = service.FindById(id.Value);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // GET: Rack/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rack/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("RackID,Name")] Rack rack)
        {
            if (ModelState.IsValid)
            {
                service.Update(rack);
                return RedirectToAction(nameof(Index));
            }
            return View(rack);
        }

        // GET: Rack/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rack rack = service.FindById(id.Value);
            if (rack == null)
            {
                return NotFound();
            }
            return View(rack);
        }

        // POST: Rack/Edit/       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("RackID,Name")] Rack rack)
        {
            if (id != rack.RackID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(rack);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RackExists(rack.RackID))
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
            return View(rack);
        }

        // GET: Rack/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Rack rack = service.FindById(id.Value);

            Tuple<long, Rack, List<RackLocation>> rack = service.GetAllRacksWithRackLocations(id.Value);

            if (rack == null)
            {
                return NotFound();
            }

            int qtyRackLocation = rack.Item3.Count();

            ViewData["Qty"] = qtyRackLocation != 0 ? qtyRackLocation.ToString() : "0";
            ViewData["ListRackLocations"] = new List<RackLocation>(rack.Item3);

            return View(rack.Item2);
        }

        // POST: Rack/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RackExists(long id)
        {
            return service.RackExists(id);
        }
    }
}
