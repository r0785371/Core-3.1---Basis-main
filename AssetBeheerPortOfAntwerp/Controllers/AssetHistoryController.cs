using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class AssetHistoryController : Controller
    {
        private readonly IAssetHistoryService service;

        public AssetHistoryController(IAssetHistoryService _service)
        {
            service = _service;
        }

        // GET: AssetHistory
        public IActionResult Index()
        {
            List<AssetHistory> externCompanies = service.GetAllAssetHistories();

            return View(externCompanies);
        }

        // GET: AssetHistory/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetHistory assetHistory = service.FindById(id.Value);
            if (assetHistory == null)
            {
                return NotFound();
            }

            return View(assetHistory);
        }

        // GET: AssetHistory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AssetHistoryID,AssetID,StatusID,Datum,NameUser,Description")] AssetHistory assetHistory)
        {
            if (ModelState.IsValid)
            {
                service.Add(assetHistory);

                return RedirectToAction(nameof(Index));
            }
            return View(assetHistory);
        }

        // GET: AssetHistory/Edit/
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssetHistory assetHistory = service.FindById(id.Value);

            if (assetHistory == null)
            {
                return NotFound();
            }

            return View(assetHistory);
        }

        // POST: AssetHistory/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("AssetHistoryID, AssetID, StatusID, Datum, NameUser, Description")] AssetHistory assetHistory)
        {
            if (id != assetHistory.AssetHistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(assetHistory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetHistoryExists(assetHistory.AssetHistoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Asset", new { id = assetHistory.AssetID });
            }
            return RedirectToAction("Edit", "Asset", new { id = assetHistory.AssetID });
        }

        // GET: AssetHistory/Delete/
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //AssetHistory assetHistory = service.FindById(id.Value);
            Tuple<long, AssetHistory, Asset> assetHistory = service.GetAssetHistoryWithSub(id.Value);

            if (assetHistory == null)
            {
                return NotFound();
            }

            ViewData["Assets"] = assetHistory.Item3;

            return View(assetHistory.Item2);
        }

        // POST: AssetHistory/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id,long assetID)
        {
            service.Remove(id);

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Edit", "Asset", new { id = assetID });
        }

        private bool AssetHistoryExists(long id)
        {
            return service.AssetHistoryExists(id);
        }
    }
}
