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
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService service;

        public WarehouseController(IWarehouseService _service)
        {
            service = _service;
        }

        // GET: Warehouse
        public IActionResult Index()
        {
            List<Warehouse> warehouses = service.GetAllWarehouses();
            ViewData["LocationId"] = new List<SelectListItem>(service.GetListLocationsIsWarehouse());

            return View(warehouses);
        }

        // GET: Warehouse/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse warehouse = service.FindById(id.Value);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // GET: Warehouse/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["LocationId"] = new List<SelectListItem>(service.GetListLocationsIsWarehouse());
            return View();
        }

        // POST: Warehouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("WarehouseID,Ref,Name,LocationId")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                service.Add(warehouse);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new List<SelectListItem>(service.GetListLocationsIsWarehouse());
            return View(warehouse);
        }

        // GET: Warehouse/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Warehouse warehouse = service.FindById(id.Value);

            Tuple<long, Warehouse, List<Asset>> warehouse = service.GetWarehouseWithAssets(id.Value);

            if (warehouse == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new List<SelectListItem>(service.GetListLocationsIsWarehouse());
            ViewData["ListAssets"] = new List<Asset>(warehouse.Item3);

            return View(warehouse.Item2);
        }

        // POST: Warehouse/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("WarehouseID,Ref,Name,LocationId")] Warehouse warehouse)
        {
            if (id != warehouse.WarehouseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(warehouse);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.WarehouseID))
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
            ViewData["LocationId"] = new List<SelectListItem>(service.GetListLocationsIsWarehouse());
            return View(warehouse);
        }

        // GET: Warehouse/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Warehouse warehouse = service.FindById(id.Value);
            Tuple<long, Warehouse, List<Asset>> warehouse = service.GetWarehouseWithAssets(id.Value);

            if (warehouse == null)
            {
                return NotFound();
            }
            int qtyAsset = warehouse.Item3.Count();

            ViewData["Qty"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(warehouse.Item3);

            return View(warehouse.Item2);
        }

        // POST: Warehouse/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseExists(long id)
        {
            return service.WarehouseExists(id);
        }
    }
}
