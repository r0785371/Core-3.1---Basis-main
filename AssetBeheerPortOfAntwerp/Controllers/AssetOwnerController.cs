using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class AssetOwnerController : Controller
    {
        private readonly DataContext _context;

        public AssetOwnerController(DataContext context)
        {
            _context = context;
        }

        // GET: AssetOwner
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.AssetOwners.Include(a => a.GroupPeople).Include(a => a.OperationalSite).Include(a => a.People).Include(a => a.Warehouse);
            return View(await dataContext.ToListAsync());
        }

        // GET: AssetOwner/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetOwner = await _context.AssetOwners
                .Include(a => a.GroupPeople)
                .Include(a => a.OperationalSite)
                .Include(a => a.People)
                .Include(a => a.Warehouse)
                .FirstOrDefaultAsync(m => m.AssetOwnerID == id);
            if (assetOwner == null)
            {
                return NotFound();
            }

            return View(assetOwner);
        }

        // GET: AssetOwner/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["GroupPeopleId"] = new SelectList(_context.GroupsPeople, "GroupPeopleID", "GroupName");
            ViewData["OperationalSiteId"] = new SelectList(_context.OperationalSites, "OperationalSiteID", "Ref");
            ViewData["PersonId"] = new SelectList(_context.People, "PersonID", "FirstName");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseID", "Name");
            return View();
        }

        // POST: AssetOwner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public async Task<IActionResult> Create([Bind("AssetOwnerID,OperationalSiteId,WarehouseId,PersonId,GroupPeopleId")] AssetOwner assetOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupPeopleId"] = new SelectList(_context.GroupsPeople, "GroupPeopleID", "GroupName", assetOwner.GroupPeopleId);
            ViewData["OperationalSiteId"] = new SelectList(_context.OperationalSites, "OperationalSiteID", "Ref", assetOwner.OperationalSiteId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonID", "FirstName", assetOwner.PersonId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseID", "Name", assetOwner.WarehouseId);
            return View(assetOwner);
        }

        // GET: AssetOwner/Edit/5
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetOwner = await _context.AssetOwners.FindAsync(id);
            if (assetOwner == null)
            {
                return NotFound();
            }
            ViewData["GroupPeopleId"] = new SelectList(_context.GroupsPeople, "GroupPeopleID", "GroupName", assetOwner.GroupPeopleId);
            ViewData["OperationalSiteId"] = new SelectList(_context.OperationalSites, "OperationalSiteID", "Ref", assetOwner.OperationalSiteId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonID", "FirstName", assetOwner.PersonId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseID", "Name", assetOwner.WarehouseId);
            return View(assetOwner);
        }

        // POST: AssetOwner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public async Task<IActionResult> Edit(long id, [Bind("AssetOwnerID,OperationalSiteId,WarehouseId,PersonId,GroupPeopleId")] AssetOwner assetOwner)
        {
            if (id != assetOwner.AssetOwnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetOwnerExists(assetOwner.AssetOwnerID))
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
            ViewData["GroupPeopleId"] = new SelectList(_context.GroupsPeople, "GroupPeopleID", "GroupName", assetOwner.GroupPeopleId);
            ViewData["OperationalSiteId"] = new SelectList(_context.OperationalSites, "OperationalSiteID", "Ref", assetOwner.OperationalSiteId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonID", "FirstName", assetOwner.PersonId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseID", "Name", assetOwner.WarehouseId);
            return View(assetOwner);
        }

        // GET: AssetOwner/Delete/5
        [Authorize(Roles = "Administrator,UserCRUD")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetOwner = await _context.AssetOwners
                .Include(a => a.GroupPeople)
                .Include(a => a.OperationalSite)
                .Include(a => a.People)
                .Include(a => a.Warehouse)
                .FirstOrDefaultAsync(m => m.AssetOwnerID == id);
            if (assetOwner == null)
            {
                return NotFound();
            }

            return View(assetOwner);
        }

        // POST: AssetOwner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var assetOwner = await _context.AssetOwners.FindAsync(id);
            _context.AssetOwners.Remove(assetOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetOwnerExists(long id)
        {
            return _context.AssetOwners.Any(e => e.AssetOwnerID == id);
        }
    }
}
