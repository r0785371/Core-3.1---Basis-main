using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();

        Tuple<long, Department, List<Person>> GetAllDepartmentWithPerson(long departmentID);

        Department FindById(long id);

        bool DepartmentExists(long id);

        void Update(Department department);

        void Add(Department department);

        void Remove(long id);

        void Save();
    }
}
