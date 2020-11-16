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
    public class LocationController : Controller
    {
        private readonly ILocationService service;

        public LocationController(ILocationService _service)
        {
            service = _service;
        }

        // GET: Location
        public IActionResult Index()
        {
            List<Location> locations = service.GetAllLocations();
            return View(locations);
        }

        // GET: Location/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location location = service.FindById(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new List<SelectListItem>(service.GetSelectListBuildings());
            ViewData["FloorId"] = new List<SelectListItem>(service.GetSelectListFloors());
            ViewData["RoomId"] = new List<SelectListItem>(service.GetSelectListRooms());
            return View();
        }

        // POST: Location/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("LocationID,BuildingId,FloorId,RoomId,RoomNo,IsWarehouse")] Location location)
        {
            if (ModelState.IsValid)
            {
                service.Add(location);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new List<SelectListItem>(service.GetSelectListBuildings());
            ViewData["FloorId"] = new List<SelectListItem>(service.GetSelectListFloors());
            ViewData["RoomId"] = new List<SelectListItem>(service.GetSelectListRooms());
            return View(location);
        }

        // GET: Location/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location location = service.FindById(id.Value);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new List<SelectListItem>(service.GetSelectListBuildings());
            ViewData["FloorId"] = new List<SelectListItem>(service.GetSelectListFloors());
            ViewData["RoomId"] = new List<SelectListItem>(service.GetSelectListRooms());
            return View(location);
        }

        // POST: Location/Edit/       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("LocationID,BuildingId,FloorId,RoomId,RoomNo,IsWarehouse")] Location location)
        {
            if (id != location.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(location);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationID))
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
            ViewData["BuildingId"] = new List<SelectListItem>(service.GetSelectListBuildings());
            ViewData["FloorId"] = new List<SelectListItem>(service.GetSelectListFloors());
            ViewData["RoomId"] = new List<SelectListItem>(service.GetSelectListRooms());
            return View(location);
        }

        // GET: Location/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location location = service.FindById(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(long id)
        {
            return service.LocationExists(id);
        }
    }
}
