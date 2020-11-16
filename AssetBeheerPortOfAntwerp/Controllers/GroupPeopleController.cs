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
    public class GroupPeopleController : Controller
    {
        private readonly IGroupPeopleService service;

        public GroupPeopleController(IGroupPeopleService _service)
        {
            service = _service;
        }

        // GET: GroupPeople
        public IActionResult Index()
        {
            List<GroupPeople> listGroupPeople = service.GetAllGroupsPeople();

            ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());

            return View(listGroupPeople);
        }

        // GET: GroupPeople/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupPeople groupPeople = service.FindById(id.Value);
            if (groupPeople == null)
            {
                return NotFound();
            }

            ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());

            return View(groupPeople);
        }

        // GET: GroupPeople/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());

            return View();
        }

        // POST: GroupPeople/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("GroupPeopleID,Ref,GroupName,PersonId")] GroupPeople groupPeople)
        {
            if (ModelState.IsValid)
            {
                service.Add(groupPeople);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());

            return View(groupPeople);
        }

        // GET: GroupPeople/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var groupPeople = service.FindById(id.Value);
            Tuple<long, GroupPeople, List<PersonGroupPeople>, List<Asset>> groupPeople = service.GetGroupPeopleWithSub(id.Value);

            if (groupPeople == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());
            ViewData["ListGroupPeople"] = new List<PersonGroupPeople>(groupPeople.Item3);
            ViewData["ListAssets"] = new List<Asset>(groupPeople.Item4);

            return View(groupPeople.Item2);
        }

        // POST: GroupPeople/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("GroupPeopleID,Ref,GroupName,PersonId")] GroupPeople groupPeople)
        {
            if (id != groupPeople.GroupPeopleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(groupPeople);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupPeopleExists(groupPeople.GroupPeopleID))
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
            ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());

            return View(groupPeople);
        }

        // GET: GroupPeople/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //GroupPeople groupPeople = service.FindById(id.Value);
            Tuple<long, GroupPeople, List<PersonGroupPeople>, List<Asset>> groupPeople = service.GetGroupPeopleWithSub(id.Value);


            if (groupPeople == null)
            {
                return NotFound();
            }

            int qtyPersonGroupPeople = groupPeople.Item3.Count();
            int qtyAsset = groupPeople.Item4.Count();
            int qty = qtyPersonGroupPeople + qtyAsset;

            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyPeople"] = qtyPersonGroupPeople != 0 ? qtyPersonGroupPeople.ToString() : "0";
            ViewData["ListGroupPeople"] = new List<PersonGroupPeople>(groupPeople.Item3);

            ViewData["QtyAssets"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(groupPeople.Item4);

            //ViewData["PersonID"] = new List<SelectListItem>(service.GetSelectListPeople());

            return View(groupPeople.Item2);
        }

        // POST: GroupPeople/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupPeopleExists(long id)
        {
            return service.GroupPeopleExists(id);
        }
    }
}
