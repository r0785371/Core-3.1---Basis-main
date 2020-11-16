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
using BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class OperationalSiteController : Controller
    {
        private readonly IOperationalSiteService service;

        public OperationalSiteController(IOperationalSiteService _service)
        {
            service = _service;
        }

        // GET: OperationalSite
        public IActionResult Index()
        {
            List<OperationalSite> operationalSites = service.GetAllOperationalSites();
            return View(operationalSites);
        }

        // GET: OperationalSite/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationalSite operationalSite = service.FindById(id.Value);
            if (operationalSite == null)
            {
                return NotFound();
            }

            return View(operationalSite);
        }

        // GET: OperationalSite/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["OperationalSiteGroupId"] = new List<SelectListItem>(service.GetSelectListOperationalSiteGroups());
            return View();
        }

        // POST: OperationalSite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("OperationalSiteID,Ref,Name,IsGroup,OperationalSiteGroupId")] OperationalSite operationalSite)
        {
            if (ModelState.IsValid)
            {
                service.Add(operationalSite);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperationalSiteGroupId"] = new List<SelectListItem>(service.GetSelectListOperationalSiteGroups());
            return View(operationalSite);
        }

        // GET: OperationalSite/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationalSiteViewModel operationalSite = service.GetSpecificOperationalSiteAndAllSubForms(id.Value);
            if (operationalSite == null)
            {
                return NotFound();
            }
            ViewData["OperationalSiteGroupId"] = new List<SelectListItem>(service.GetSelectListOperationalSiteGroups());
            return View(operationalSite);
        }

        // POST: OperationalSite/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, bool? goToOperationSiteLocation, OperationalSiteViewModel operationalSiteViewModel, [Bind("OperationalSiteID,Ref,Name,IsGroup,OperationalSiteGroupId")] OperationalSite operationalSite)
        {
            if (id != operationalSite.OperationalSiteID)
            {
                return NotFound();
            }

            // if we want to change the OperationalSiteGroupId back to "null"
            // the data comes back from the view via operationalSiteViewModel but need to be saved to operationalSite,

            // so, a) we need to actualised the OperationalSiteGroupId in operationalSite and ...
            operationalSite.OperationalSiteGroupId = operationalSiteViewModel.operationalSite.OperationalSiteGroupId;

            // ... b) we need to switch the ModelState.
            ModelState.Clear();
            TryValidateModel(operationalSite);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(operationalSite);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationalSiteExists(operationalSite.OperationalSiteID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (goToOperationSiteLocation == true)
                {
                    return RedirectToAction("Create", "OperationalSiteLocation", new { operationalSiteID = operationalSite.OperationalSiteID });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["OperationalSiteGroupId"] = new List<SelectListItem>(service.GetSelectListOperationalSiteGroups());
            return View(operationalSite);
        }

        // GET: OperationalSite/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //OperationalSite operationalSite = service.FindById(id.Value);

            Tuple<long, OperationalSite, List<Asset>> operationalSite = service.GetOperationalSiteWithAssets(id.Value);


            int qtyAsset = operationalSite.Item3.Count();

            int qty = qtyAsset;

            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyAssets"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(operationalSite.Item3);

            if (operationalSite == null)
            {
                return NotFound();
            }

            return View(operationalSite.Item2);
        }

        // POST: OperationalSite/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OperationalSiteExists(long id)
        {
            return service.OperationalSiteExists(id);
        }
    }
}
