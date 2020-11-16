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

namespace PortOfAntwerpAppAssets.Controllers
{
    public class ExternCompanyController : Controller
    {
        private readonly IExternCompanyService service;

        public ExternCompanyController(IExternCompanyService _service)
        {
            service = _service;
        }

        // GET: ExternCompany
        public IActionResult Index()
        {
            List<ExternCompany> externCompanies = service.GetAllExternCompanies();

            return View(externCompanies);
        }

        // GET: ExternCompany/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExternCompany externCompany = service.FindById(id.Value);
            if (externCompany == null)
            {
                return NotFound();
            }

            return View(externCompany);
        }

        // GET: ExternCompany/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExternCompany/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ExternCompanyID,Ref,Name,Street,Number,Postcode,City,State,Country,Tel,Mail,WebSite,IsActief")] ExternCompany externCompany)
        {
            if (ModelState.IsValid)
            {
                service.Add(externCompany);

                return RedirectToAction(nameof(Index));
            }
            return View(externCompany);
        }

        // GET: ExternCompany/Edit/
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //ExternCompany externCompany = service.FindById(id.Value);

            Tuple<long, ExternCompany, List<Asset>> externCompany = service.GetExternCompanyWithSub(id.Value);

            if (externCompany == null)
            {
                return NotFound();
            }

            ViewData["ListAssets"] = new List<Asset>(externCompany.Item3);

            
            return View(externCompany.Item2);
        }

        // POST: ExternCompany/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("ExternCompanyID,Ref,Name,Street,Number,Postcode,City,State,Country,Tel,Mail,WebSite,IsActief")] ExternCompany externCompany)
        {
            if (id != externCompany.ExternCompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(externCompany);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExternCompanyExists(externCompany.ExternCompanyID))
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
            return View(externCompany);
        }

        // GET: ExternCompany/Delete/
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //ExternCompany externCompany = service.FindById(id.Value);
            Tuple<long, ExternCompany, List<Asset>> externCompany = service.GetExternCompanyWithSub(id.Value);

            if (externCompany == null)
            {
                return NotFound();
            }

            ViewData["ListAssets"] = new List<Asset>(externCompany.Item3);

            return View(externCompany.Item2);
        }

        // POST: ExternCompany/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ExternCompanyExists(long id)
        {
            return service.ExternCompanyExists(id);
        }
    }
}
