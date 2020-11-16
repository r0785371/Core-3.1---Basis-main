using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
   public class PersonRepository: IPersonRepository
    {
        readonly DataContext context;

        public PersonRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Person> GetAllPersons()
        {
            return context.People
                .Include(p => p.Department)
                .Include(p => p.PersonGroupPeoples)
                    .ThenInclude(p => p.Person)
                .ToList();
        }

        public List<SelectListItem> GetSelectListPeople()
        {
            return context.People.Select(s => new SelectListItem
            {
                Value = s.PersonID.ToString(),
                Text = s.FullName,
                //Selected=c.PersonID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public List<Person> GetAllPersonsOfDepartment(long departmentID)
        {
            return context.People
                .Where(p => p.DepartmentId == departmentID)
                .Include(p => p.Department)
                .Include(p => p.PersonGroupPeoples)
                    .ThenInclude(p => p.Person)
                .OrderBy(p => p.FullName)
                .ToList();
        }

        //public List<Person> GetAllPersonsOfGroupPeople(long groupPeopleID)
        //{
        //    return context.People
        //        .Where(p => p.PersonGroupPeoples.Any(g => g.GroupPeopleID == groupPeopleID))
        //        .Include(p => p.Department)
        //        .Include(p => p.PersonGroupPeoples)
        //            .ThenInclude(p => p.Person)
        //        .OrderBy(p => p.FullName)
        //        .ToList();
        //}

        public Person FindById(long id)
        {
            return context.People
                .Where(s => s.PersonID == id)
                .Include(p => p.Department)
                .Include(p => p.PersonGroupPeoples)
                    .ThenInclude(p => p.Person)
                .Single();
        }

        public bool PersonExists(long id)
        {
            return context.People.Any(s => s.PersonID == id);
        }

        public long Add(Person person)
        {
            context.People.Add(person);
            context.SaveChanges();
            return person.PersonID;
        }

        public void Update(Person person)
        {
            context.People.Update(person);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var person = context.People.SingleOrDefault(s => s.PersonID == id);
            context.People.Remove(person);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
