using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IPersonRepository
    {
        List<Person> GetAllPersons();

        List<SelectListItem> GetSelectListPeople();

        List<Person> GetAllPersonsOfDepartment(long departmentID);

        //List<Person> GetAllPersonsOfGroupPeople(long groupPeopleID);

        Person FindById(long id);

        bool PersonExists(long id);

        void Update(Person person);

        long Add(Person person);

        void Remove(long id);

        void Save();
    }
}
