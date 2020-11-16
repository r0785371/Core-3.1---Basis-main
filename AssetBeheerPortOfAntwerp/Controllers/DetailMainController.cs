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
    public class DetailMainController : Controller
    {
        private readonly IDetailMainService service;

        public DetailMainController(IDetailMainService _service)
        {
            service = _service;
        }

        // GET: DetailMain
        public IActionResult Index()
        {
            List<DetailMain> detailMains = service.GetAllDetailMains();
            return View(detailMains);
        }

        // GET: DetailMain/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailMain detailMain = service.FindById(id.Value);
            if (detailMain == null)
            {
                return NotFound();
            }

            return View(detailMain);
        }

        // GET: DetailMain/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailMain/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("DetailMainID,Name")] DetailMain detailMain)
        {
            if (ModelState.IsValid)
            {
                service.Add(detailMain);
                return RedirectToAction(nameof(Index));
            }
            return View(detailMain);
        }

        // GET: DetailMain/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailMain detailMain = service.FindById(id.Value);
            if (detailMain == null)
            {
                return NotFound();
            }
            return View(detailMain);
        }

        // POST: DetailMain/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("DetailMainID,Name")] DetailMain detailMain)
        {
            if (id != detailMain.DetailMainID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(detailMain);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailMainExists(detailMain.DetailMainID))
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
            return View(detailMain);
        }

        // GET: DetailMain/Delete/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //DetailMain detailMain = service.FindById(id.Value);
            Tuple<long, DetailMain, List<Detail>> detailMain = service.GetAllDetailMainWithDetails(id.Value);


            if (detailMain.Item2 == null)
            {
                return NotFound();
            }


            int qtyDetails = detailMain.Item3.Count();

            int qty = qtyDetails;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyDetails"] = qtyDetails != 0 ? qtyDetails.ToString() : "0";
            ViewData["ListDetails"] = new List<Detail>(detailMain.Item3);

            return View(detailMain.Item2);
        }

        // POST: DetailMain/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DetailMainExists(long id)
        {
            return service.DetailMainExists(id);
        }
    }
}
