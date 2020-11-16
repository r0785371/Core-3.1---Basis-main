using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DepartmentService: IDepartmentService
    {
        readonly IDepartmentRepository repository;
        readonly IPersonRepository repositoryPerson;

        public DepartmentService(IDepartmentRepository _repository, IPersonRepository _repositoryPerson)
        {
            repository = _repository;
            repositoryPerson = _repositoryPerson;
        }

        public List<Department> GetAllDepartments()
        {
            return repository.GetAllDepartments();
        }

        public Tuple<long, Department, List<Person>> GetAllDepartmentWithPerson(long departmentID)
        {
            Department department = FindById(departmentID);

            List<Person> people = repositoryPerson.GetAllPersonsOfDepartment(departmentID);


            return new Tuple<long, Department, List<Person>>(departmentID, department, people);
        }

        public Department FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool DepartmentExists(long id)
        {
            return repository.DepartmentExists(id);
        }

        public void Update(Department department)
        {
            repository.Update(department);
        }

        public void Add(Department department)
        {
            repository.Add(department);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
