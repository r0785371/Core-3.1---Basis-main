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
    public class StatusController : Controller
    {
        private readonly IStatusService service;

        public StatusController(IStatusService _service)
        {
            service = _service;
        }

        // GET: Status
        public IActionResult Index()
        {
            List<Status> statuses = service.GetAllStatus();
            return View(statuses);
        }

        // GET: Status/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Status status = service.FindById(id.Value);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("Name,Description,HasProduct,ProductSequence,NoSupport,HasPurchase,PurchaseSequence,GenerateAssetOrLicense,HasAsset,AssetSequence,HasLicense,LicenceSequence,ToOrder,Ordered,OnStock,InUse,OutOfUse")] Status status)
        {
            if (ModelState.IsValid)
            {
                service.Add(status);
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Status/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Status status = service.FindById(id.Value);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("StatusID,Name,Description,HasProduct,ProductSequence,NoSupport,HasPurchase,PurchaseSequence,GenerateAssetOrLicense,HasAsset,AssetSequence,HasLicense,LicenceSequence,ToOrder,Ordered,OnStock,InUse,OutOfUse")] Status status)
        {
            if (id != status.StatusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.StatusID))
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
            return View(status);
        }

        // GET: Status/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Status status = service.FindById(id.Value);

            Tuple<long, Status, List<Hardware>, List<Software>, List<PurchaseItem>, List<License>, List<Asset>> status = service.GetStatusWithRelatedSubs(id.Value);

            if (status.Item2 == null)
            {
                return NotFound();
            }

            int qtyHardware = status.Item3.Count();
            int qtySoftware = status.Item4.Count();
            int qtyPurchaseItem = status.Item5.Count();
            int qtyLicense = status.Item6.Count();
            int qtyAsset = status.Item7.Count();

            int qty = qtyAsset + qtyHardware + qtyLicense + qtyPurchaseItem + qtySoftware;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyHardware"] = qtyHardware != 0 ? qtyHardware.ToString() : "0";
            ViewData["ListHardware"] = new List<Hardware>(status.Item3);

            ViewData["QtySoftware"] = qtySoftware != 0 ? qtySoftware.ToString() : "0";
            ViewData["ListSoftware"] = new List<Software>(status.Item4);

            ViewData["QtyPurchaseItem"] = qtyPurchaseItem != 0 ? qtyPurchaseItem.ToString() : "0";
            ViewData["ListPurchaseItem"] = new List<PurchaseItem>(status.Item5);

            ViewData["QtyLicense"] = qtyLicense != 0 ? qtyLicense.ToString() : "0";
            ViewData["ListLicense"] = new List<License>(status.Item6);

            ViewData["QtyAsset"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAsset"] = new List<Asset>(status.Item7);

            return View(status.Item2);
        }

        // POST: Status/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(long id)
        {
            return service.StatusExists(id);
        }
    }
}
