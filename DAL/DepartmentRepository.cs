using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DepartmentRepository: IDepartmentRepository
    {
        readonly DataContext context;

        public DepartmentRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Department> GetAllDepartments()
        {
           return context.Departments
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListDepartments()
        {
            return context.Departments.Select(s => new SelectListItem
            {
                Value = s.DepartmentID.ToString(),
                Text = s.Name,
                //Selected=c.DepartmentID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public Department FindById(long id)
        {
            return context.Departments.Where(s => s.DepartmentID == id).Single();
        }

        public bool DepartmentExists(long id)
        {
            return context.Departments.Any(s => s.DepartmentID == id);
        }

        public void Add(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var department = context.Departments.SingleOrDefault(s => s.DepartmentID == id);
            context.Departments.Remove(department);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
