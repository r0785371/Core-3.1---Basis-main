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
    public class FloorController : Controller
    {
        private readonly IFloorService service;

        public FloorController(IFloorService _service)
        {
            service = _service;
        }

        // GET: Floor
        public IActionResult Index()
        {
            List<Floor> floors = service.GetAllFloors();
            return View(floors);
        }

        // GET: Floor/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Floor floor = service.FindById(id.Value);
            if (floor == null)
            {
                return NotFound();
            }

            return View(floor);
        }

        // GET: Floor/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Floor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("FloorID,Ref,Name,Sequence")] Floor floor)
        {
            if (ModelState.IsValid)
            {
                service.Add(floor);
                return RedirectToAction(nameof(Index));
            }
            return View(floor);
        }

        // GET: Floor/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Floor floor = service.FindById(id.Value);
            if (floor == null)
            {
                return NotFound();
            }
            return View(floor);
        }

        // POST: Floor/Edit/       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("FloorID,Ref,Name,Sequence")] Floor floor)
        {
            if (id != floor.FloorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(floor);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FloorExists(floor.FloorID))
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
            return View(floor);
        }

        // GET: Floor/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Floor floor = service.FindById(id.Value);

            Tuple<long, Floor, List<Location>> floor = service.GetAllFloorsWithLocations(id.Value);

            if (floor.Item2 == null)
            {
                return NotFound();
            }

            int qtyLocation = floor.Item3.Count();

            ViewData["Qty"] = qtyLocation != 0 ? qtyLocation.ToString() : "0";
            ViewData["ListLocations"] = new List<Location>(floor.Item3);

            return View(floor.Item2);
        }

        // POST: Floor/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FloorExists(long id)
        {
            return service.FloorExists(id);
        }
    }
}
