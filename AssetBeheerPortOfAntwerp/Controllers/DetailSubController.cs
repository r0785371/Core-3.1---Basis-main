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
    public class DetailSubController : Controller
    {
        private readonly IDetailSubService service;

        public DetailSubController(IDetailSubService _service)
        {
            service = _service;
        }

        // GET: DetailSub
        public IActionResult Index()
        {
            List<DetailSub> detailSubs = service.GetAllDetailSubs();
            return View(detailSubs);
        }

        // GET: DetailSub/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailSub detailSub = service.FindById(id.Value);
            if (detailSub == null)
            {
                return NotFound();
            }

            return View(detailSub);
        }

        // GET: DetailSub/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailSub/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("DetailSubID,Name")] DetailSub detailSub)
        {
            if (ModelState.IsValid)
            {
                service.Add(detailSub);
                return RedirectToAction(nameof(Index));
            }
            return View(detailSub);
        }

        // GET: DetailSub/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailSub detailSub = service.FindById(id.Value);
            if (detailSub == null)
            {
                return NotFound();
            }
            return View(detailSub);
        }

        // POST: DetailSub/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("DetailSubID,Name")] DetailSub detailSub)
        {
            if (id != detailSub.DetailSubID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(detailSub);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailSubExists(detailSub.DetailSubID))
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
            return View(detailSub);
        }

        // GET: DetailSub/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //DetailSub detailSub = service.FindById(id.Value);

            Tuple<long, DetailSub, List<Detail>> detailSub = service.GetAllDetailSubWithDetails(id.Value);

            if (detailSub.Item2 == null)
            {
                return NotFound();
            }

            int qtyDetails = detailSub.Item3.Count();

            int qty = qtyDetails;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyDetails"] = qtyDetails != 0 ? qtyDetails.ToString() : "0";
            ViewData["ListDetails"] = new List<Detail>(detailSub.Item3);

            return View(detailSub.Item2);
        }

        // POST: DetailSub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DetailSubExists(long id)
        {
            return service.DetailSubExists(id);
        }
    }
}
