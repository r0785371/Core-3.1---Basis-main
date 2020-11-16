using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IDepartmentRepository
    {
        List<Department> GetAllDepartments();

        List<SelectListItem> GetSelectListDepartments();

        Department FindById(long id);

        bool DepartmentExists(long id);

        void Update(Department department);

        void Add(Department department);

        void Remove(long id);

        void Save();
    }
}
