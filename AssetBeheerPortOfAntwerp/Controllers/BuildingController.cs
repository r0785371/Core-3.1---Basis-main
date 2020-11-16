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
    public class BuildingController : Controller
    {
        private readonly IBuildingService service;

        public BuildingController(IBuildingService _service)
        {
            service = _service;
        }
        
        // GET: Building
        public IActionResult Index()
        {
            List<Building> buildings = service.GetAllBuildings();
            return View(buildings);
        }

        // GET: Building/Details
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = service.FindById(id.Value);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // GET: Building/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Building/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("BuildingID,Ref,Name,Street,Number,Zip code,City")] Building building)
        {
            if (ModelState.IsValid)
            {
                service.Add(building);
                return RedirectToAction(nameof(Index));
            }
            return View(building);
        }

        // GET: Building/Edit
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Building building = service.FindById(id.Value);

            Tuple<long, Building, List<Location>> building = service.GetAllBuildingsWithLocations(id.Value);

            if (building == null)
            {
                return NotFound();
            }

            int qtyLocation = building.Item3.Count();

            ViewData["Qty"] = qtyLocation != 0 ? qtyLocation.ToString() : "0";
            ViewData["ListLocations"] = new List<Location>(building.Item3);

            return View(building.Item2);
        }

        // POST: Building/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("BuildingID,Ref,Name,Street,Number,Zip code,City")] Building building)
        {
            if (id != building.BuildingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(building);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(building.BuildingID))
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
            return View(building);
        }


        // GET: Building/Delete/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Building building = service.FindById(id.Value);

            Tuple<long, Building, List<Location>> building = service.GetAllBuildingsWithLocations(id.Value);


            if (building == null)
            {
                return NotFound();
            }

            int qtyBuilding = building.Item3.Count();

            ViewData["Qty"] = qtyBuilding != 0 ? qtyBuilding.ToString() : "0";
            ViewData["ListLocations"] = new List<Location>(building.Item3);

            return View(building.Item2);
        }

        // POST: Building/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingExists(long id)
        {
            return service.BuildingExists(id);
        }
    }
}
