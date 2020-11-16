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
    public class WarningPeriodController : Controller
    {
        private readonly IWarningPeriodService service;

        public WarningPeriodController(IWarningPeriodService _service)
        {
            service = _service;
        }

        // GET: WarningPeriod
        public IActionResult Index()
        {
            List<WarningPeriod> warningPeriods = service.GetAllWarningPeriods();
            return View(warningPeriods);
        }

        // GET: WarningPeriod/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WarningPeriod warningPeriod = service.FindById(id.Value);
            if (warningPeriod == null)
            {
                return NotFound();
            }

            return View(warningPeriod);
        }

        // GET: WarningPeriod/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: WarningPeriod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]

        public IActionResult Create([Bind("WarningPeriodID,WarningPeriodMonth,Name,Description,Standard")] WarningPeriod warningPeriod)
        {
            if (ModelState.IsValid)
            {
                service.Add(warningPeriod);
                return RedirectToAction(nameof(Index));
            }
            return View(warningPeriod);
        }

        // GET: WarningPeriod/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WarningPeriod warningPeriod = service.FindById(id.Value);
            if (warningPeriod == null)
            {
                return NotFound();
            }
            return View(warningPeriod);
        }

        // POST: WarningPeriod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]

        public IActionResult Edit(long id, [Bind("WarningPeriodID,WarningPeriodMonth,Name,Description,Standard")] WarningPeriod warningPeriod)
        {
            if (id != warningPeriod.WarningPeriodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(warningPeriod);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarningPeriodExists(warningPeriod.WarningPeriodID))
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
            return View(warningPeriod);
        }

        // GET: WarningPeriod/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]

        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //WarningPeriod warningPeriod = service.FindById(id.Value);
            Tuple<long, WarningPeriod, List<Asset>, List<ProductType>> warningPeriod = service.GetAllWarningPeriodsWithSubs(id.Value);

            if (warningPeriod == null)
            {
                return NotFound();
            }

            int qtyAssets = warningPeriod.Item3.Count();
            int qtyProductTypes = warningPeriod.Item4.Count();

            int qty = qtyAssets + qtyProductTypes;
            ViewData["Qty"] = qty >= 0 ? qty.ToString() : "-1";

            ViewData["QtyAssets"] = qtyAssets != 0 ? qtyAssets.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(warningPeriod.Item3);

            ViewData["QtyProductTypes"] = qtyProductTypes != 0 ? qtyProductTypes.ToString() : "0";
            ViewData["ListProductTypes"] = new List<ProductType>(warningPeriod.Item4);

            return View(warningPeriod.Item2);
        }

        // POST: WarningPeriod/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]

        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool WarningPeriodExists(long id)
        {
            return service.WarningPeriodExists(id);
        }
    }
}
