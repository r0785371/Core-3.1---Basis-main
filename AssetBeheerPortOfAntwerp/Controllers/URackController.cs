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
    public class URackController : Controller
    {
        private readonly IURackService service;

        public URackController(IURackService _service)
        {
            service = _service;
        }

        // GET: URack
        public IActionResult Index()
        {
            List<URack> uRacks = service.GetAllURacks();
            return View(uRacks);
        }

        // GET: URack/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            URack uRack = service.FindById(id.Value);
            if (uRack == null)
            {
                return NotFound();
            }

            return View(uRack);
        }

        // GET: URack/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: URack/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("URackID,Name")] URack uRack)
        {
            if (ModelState.IsValid)
            {
                service.Add(uRack);
                return RedirectToAction(nameof(Index));
            }
            return View(uRack);
        }

        // GET: URack/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            URack uRack = service.FindById(id.Value);
            if (uRack == null)
            {
                return NotFound();
            }
            return View(uRack);
        }

        // POST: URack/Edit/       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("URackID,Name")] URack uRack)
        {
            if (id != uRack.URackID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(uRack);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!URackExists(uRack.URackID))
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
            return View(uRack);
        }

        // GET: URack/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //URack uRack = service.FindById(id.Value);

            Tuple<long, URack, List<Asset>> uRack = service.GetAllURacksWithAssets(id.Value);


            if (uRack == null)
            {
                return NotFound();
            }

            int qtyAsset = uRack.Item3.Count();

            ViewData["Qty"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(uRack.Item3);

            return View(uRack.Item2);
        }

        // POST: URack/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool URackExists(long id)
        {
            return service.URackExists(id);
        }
    }
}
