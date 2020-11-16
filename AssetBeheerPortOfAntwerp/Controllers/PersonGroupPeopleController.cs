using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class PersonGroupPeopleController : Controller
    {
        private readonly IPersonGroupPeopleService service;

        public PersonGroupPeopleController(IPersonGroupPeopleService _service)
        {
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonGroupPeople personGroupPeople = service.FindById(id.Value);
            if (personGroupPeople == null)
            {
                return NotFound();
            }

            return View(personGroupPeople);
        }

        // GET: PersonGroupPeople/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult CreateGroup(long personID)
        {
            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());
            ViewData["PersonID"] = personID;
            ViewData["ListPersonID"] = new List<SelectListItem>(service.GetSelectListPerson());

            return View();
        }

        // POST: PersonGroupPeople/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult CreateGroup(string personID, [Bind("PersonGroupPeopleID,PersonID,GroupPeopleID")] PersonGroupPeople personGroupPeople)
        {
            if (ModelState.IsValid)
            {
                service.Add(personGroupPeople);

                return RedirectToAction("Edit", "Person", new { id = personGroupPeople.PersonID });
            }

            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());
            ViewData["PersonID"] = personGroupPeople.PersonID;

            return View(personGroupPeople);
        }

        // GET: PersonGroupPeople/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult CreatePerson(long groupPeopleID)
        {
            ViewData["ListGroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());
            ViewData["GroupPeopleID"] = groupPeopleID;
            ViewData["ListPersonID"] = new List<SelectListItem>(service.GetSelectListPerson());

            return View();
        }

        // POST: PersonGroupPeople/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult CreatePerson(long groupPeopleID, [Bind("PersonGroupPeopleID,PersonID,GroupPeopleID")] PersonGroupPeople personGroupPeople)
        {
            if (ModelState.IsValid)
            {
                service.Add(personGroupPeople);

                return RedirectToAction("Edit", "GroupPeople", new { id = personGroupPeople.GroupPeopleID });
            }

            ViewData["ListGroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());
            ViewData["GroupPeopleID"] = personGroupPeople.PersonID;
            ViewData["ListPersonID"] = new List<SelectListItem>(service.GetSelectListPerson());

            return View(personGroupPeople);
        }

        // GET: PersonGroupPeople/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonGroupPeople personGroupPeople = service.FindById(id.Value);
            if (personGroupPeople == null)
            {
                return NotFound();
            }

            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());

            return View(personGroupPeople);
        }

        // POST: PersonGroupPeople/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("PersonGroupPeopleID,PersonID,GroupPeopleID")] PersonGroupPeople personGroupPeople)
        {
            if (id != personGroupPeople.PersonGroupPeopleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(personGroupPeople);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonGroupPeopleExists(personGroupPeople.PersonGroupPeopleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Edit", "Person", new { id = personGroupPeople.PersonID });
            }

            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());
            return View(personGroupPeople);
        }

        // GET: PersonGroupPeople/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id, bool? goToGroup, bool? goToPerson)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonGroupPeople personGroupPeople = service.FindById(id.Value);
            if (personGroupPeople == null)
            {
                return NotFound();
            }

            ViewData["GoToGroup"] = goToGroup == true ? goToGroup : false;
            ViewData["GoToPerson"] = goToPerson == true ? goToPerson : false;

            return View(personGroupPeople);
        }

        // POST: PersonGroupPeople/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id, bool? goToGroup, bool? goToPerson)
        {
            //long productID;
            PersonGroupPeople personGroupPeople = service.FindById(id);

            service.Remove(id);

            if (goToPerson == true)
            {
                return RedirectToAction("Edit", "Person", new { id = personGroupPeople.PersonID });
            }
            if (goToGroup == true)
            {
                return RedirectToAction("Edit", "GroupPeople", new { id = personGroupPeople.GroupPeopleID });
            }
            return RedirectToAction("Edit", "Person", new { id = personGroupPeople.PersonID });
        }

        private bool PersonGroupPeopleExists(long id)
        {
            return service.PersonGroupPeopleExists(id);
        }
    }
}
