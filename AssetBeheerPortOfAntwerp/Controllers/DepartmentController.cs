using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;
using BLL;
using BLL.interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService service;

        public DepartmentController(IDepartmentService _service)
        {
            service = _service;
        }

        // GET: Department
        public IActionResult Index()
        {
            List<Department> departments = service.GetAllDepartments();
            return View(departments);
        }

        // GET: Department/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = service.FindById(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("DepartmentID,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                service.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Department department = service.FindById(id.Value);

            Tuple<long, Department, List<Person>> department = service.GetAllDepartmentWithPerson(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            ViewData["ListPeople"] = new List<Person>(department.Item3);

            return View(department.Item2);
        }

        // POST: Department/Edit/        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("DepartmentID,Name")] Department department)
        {
            if (id != department.DepartmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentID))
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
            return View(department);
}

        // GET: Department/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Department department = service.FindById(id.Value);

            Tuple<long, Department, List<Person>> department = service.GetAllDepartmentWithPerson(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            int qtyPerson = department.Item3.Count();

            int qty = qtyPerson;
            ViewData["Qty"] = qty != 0 ? qty.ToString() : "0";

            ViewData["QtyPerson"] = qtyPerson != 0 ? qtyPerson.ToString() : "0";
            ViewData["ListPeople"] = new List<Person>(department.Item3);

            return View(department.Item2);
        }

        // POST: Department/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(long id)
        {
            return service.DepartmentExists(id);
        }
    }
}
