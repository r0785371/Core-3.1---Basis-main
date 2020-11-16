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
    public class OperationalSiteLocationController : Controller
    {
        private readonly IOperationalSiteLocationService service;

        public OperationalSiteLocationController(IOperationalSiteLocationService _service)
        {
            service = _service;
        }

        // GET: OperationalSiteLocations
        public IActionResult Index()
        {
            List<OperationalSiteLocation> operationalSiteLocations 
                = service.GetAllOperationalSiteLocations();
            return View(operationalSiteLocations);
        }

        // GET: OperationalSiteLocations/Details
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationalSiteLocation operationalSiteLocation =
                service.FindById(id.Value);
            if (operationalSiteLocation == null)
            {
                return NotFound();
            }

            //return View(operationalSiteLocation);
            return RedirectToAction("Edit", "OperationalSite", new { id = operationalSiteLocation.OperationalSiteID });
        }

        // GET: OperationalSiteLocations/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(long? operationalSiteID)
        {
            OperationalSiteLocation operationalSiteLocation = new OperationalSiteLocation();
            if (operationalSiteID != null)
            {
                operationalSiteLocation.OperationalSiteID = operationalSiteID.Value;
            }
           

            ViewData["OperationalSiteID"] = new List<SelectListItem>(service.GetSelectListOperationalSite());
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocations());
                        
            return View(operationalSiteLocation);
        }

        // POST: OperationalSiteLocations/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("OperationalSiteLocationID,OperationalSiteID,LocationID")] OperationalSiteLocation operationalSiteLocation)
        {
            if (ModelState.IsValid)
            {
                service.Add(operationalSiteLocation);
                //return RedirectToAction(nameof(Index));

                return RedirectToAction("Edit", "OperationalSite", new { id = operationalSiteLocation.OperationalSiteID });


            }
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocations());
            ViewData["OperationalSiteID"] = new List<SelectListItem>(service.GetSelectListOperationalSite());
            return View(operationalSiteLocation);
        }

        // GET: OperationalSiteLocations/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id, long? operationalSiteID)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationalSiteLocation operationalSiteLocation = service.FindById(id.Value);
            if (operationalSiteLocation == null)
            {
                return NotFound();
            }

            ViewData["OperationalSiteID"] = new List<SelectListItem>(service.GetSelectOperationalSites(operationalSiteID));
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocations());
            
            return View(operationalSiteLocation);
        }

        // POST: OperationalSiteLocations/Edit       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("OperationalSiteLocationID,OperationalSiteID,LocationID")] OperationalSiteLocation operationalSiteLocation)
        {
            if (id != operationalSiteLocation.OperationalSiteLocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(operationalSiteLocation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationalSiteLocationExists(operationalSiteLocation.OperationalSiteLocationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Edit", "OperationalSite", new { id = operationalSiteLocation.OperationalSiteID });
            }
            ViewData["LocationID"] = new List<SelectListItem>(service.GetSelectListLocations());
            ViewData["OperationalSiteID"] = new List<SelectListItem>(service.GetSelectListOperationalSite());
            return View(operationalSiteLocation);
        }

        // GET: OperationalSiteLocations/Delete
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //OperationalSiteLocation operationalSiteLocation = service.FindById(id.Value);

            Tuple<long, OperationalSiteLocation, List<Asset>> operationalSiteLocation = service.GetOperationalSiteLocationWithSub(id.Value);

            if (operationalSiteLocation == null)
            {
                return NotFound();
            }

            int qtyAsset = operationalSiteLocation.Item3.Count();

            ViewData["Qty"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(operationalSiteLocation.Item3);

            return View(operationalSiteLocation.Item2);
        }

        // POST: OperationalSiteLocations/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            OperationalSiteLocation operationalSiteLocation = service.FindById(id);
            service.Remove(id);

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Edit", "OperationalSite", new { id = operationalSiteLocation.OperationalSiteID });
        }

        private bool OperationalSiteLocationExists(long id)
        {
            return service.OperationalSiteLocationExists(id); 
        }
    }
}
