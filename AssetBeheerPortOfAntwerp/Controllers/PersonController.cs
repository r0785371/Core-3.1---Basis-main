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
    public class PersonController : Controller
    {
        private readonly IPersonService service;

        public PersonController(IPersonService _service)
        {
            service = _service;
        }

        // GET: Person
        public IActionResult Index()
        {
            List<Person> people = service.GetAllPersons();
            ViewData["DepartmentID"] = new List<SelectListItem>(service.GetSelectListDepartments());
            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());

            return View(people);
        }

        // GET: Person/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person person = service.FindById(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["DepartmentID"] = new List<SelectListItem>(service.GetSelectListDepartments());
            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());

            return View(person);
        }

        // GET: Person/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new List<SelectListItem>(service.GetSelectListDepartments());
            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());

            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        //Removed PersonID from Bind as still not exist and the DB will create it!
        public IActionResult Create([Bind("Ref, FirstName,LastName,Phone,Email,DepartmentId,GroupPeopleID")] Person person)
        {
            if (ModelState.IsValid)
            {
                service.Add(person);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentID"] = new List<SelectListItem>(service.GetSelectListDepartments());
            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());

            return View(person);
        }

        // GET: Person/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Person person = service.FindById(id.Value);

            Tuple<long, Person, List<Asset>, List<PersonGroupPeople>> person = service.GetPersonWitAssets(id.Value);

            if (person == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new List<SelectListItem>(service.GetSelectListDepartments());
            ViewData["ListGroupPeople"] = new List<PersonGroupPeople>(person.Item4);
            ViewData["ListAssets"] = new List<Asset>(person.Item3);

            return View(person.Item2);
        }

        // POST: Person/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("Ref, PersonID,FirstName,LastName,Phone,Email,DepartmentId,GroupPeopleID")] Person person)
        {
            if (id != person.PersonID)
            {
                return NotFound();
            }

            // Need to clear and add ModelState new as if user set selectbox to null!
            ModelState.Clear();
            TryValidateModel(person);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonID))
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
            ViewData["DepartmentID"] = new List<SelectListItem>(service.GetSelectListDepartments());
            ViewData["GroupPeopleID"] = new List<SelectListItem>(service.GetSelectListGroupPeople());

            return View(person);
        }

        // GET: Person/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Person person = service.FindById(id.Value);

            Tuple<long, Person, List<Asset>, List<PersonGroupPeople>> person = service.GetPersonWitAssets(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            int qtyAsset = person.Item3.Count();
            int qtyPersonGroup = person.Item4.Count();

            int qty = qtyAsset + qtyPersonGroup;

            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyAssets"] = qtyAsset != 0 ? qtyAsset.ToString() : "0";
            ViewData["ListAssets"] = new List<Asset>(person.Item3);

            ViewData["QtyPersonGroups"] = qtyPersonGroup != 0 ? qtyPersonGroup.ToString() : "0";
            ViewData["ListGroupPeople"] = new List<PersonGroupPeople>(person.Item4);

            return View(person.Item2);
        }

        // POST: Person/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            // Checks if can remove the Person, if there are no Assets linked (via AssetOwner) to this person
            // then it will remove the person successful and will return Item1 = true and Item3 = 0, 
            //  otherwise returns Item1 = false, Item2 is the AssetOwnerID and Item3 = qty of assets signed to this person.

            var remove = service.Remove(id);

            // if removed successfull go to index
            if (remove.Item1 == true)
            {
                return RedirectToAction(nameof(Index));
            }

            // Else go back to delete view
            return RedirectToAction("DeleteNotPossible", new { id, assetOwnerID = remove.Item2, qtyAssets = remove.Item3 });
        }

        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteNotPossible(long id, long assetOwnerID, int qtyAssets)
        {
            if (id == null && assetOwnerID == null)
            {
                return NotFound();
            }

            Person person = service.FindById(id);
            List<Asset> assets = service.GetAllAssetsOfAssetOwner(assetOwnerID);

            if (person == null && assets == null)
            {
                return NotFound();
            }

            // If qtyAssets has no value it means that the user wants to delete this Person. Will show view 0!
            // if the value is bigger than 0, the user has tried already to delete this Person, but there are 
            // Assets linked to this Person (via AssetOwner), so it will inform user the qty of Assets linked!

            ViewData["QtyAssets"] = qtyAssets;
            ViewData["ID"] = person.PersonID;
            ViewData["Owner"] = person.FullName;

            return View(assets);
        }

        // POST: Person/DeleteNotPossible/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteNotPossible(long? id)
        {

            // if id is null
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            //Else go back to delete view
            return RedirectToAction("Delete", new { id });
        }

        private bool PersonExists(long id)
        {
            return service.PersonExists(id);
        }
    }
}
