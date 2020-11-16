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
    public class PersonGroupPeopleRepository: IPersonGroupPeopleRepository
    {
        readonly DataContext context;

        public PersonGroupPeopleRepository(DataContext _context)
        {
            context = _context;
        }

        public List<PersonGroupPeople> GetAllPersonGroupPeople()
        {
            return context.PersonGroupPeoples
                .Include(p => p.GroupPeoples)
                .Include(p => p.Person)
                    .ThenInclude(p => p.Department)
                .ToList();
        }

        //public List<PersonGroupPeople> GetAllPersonGroupPeople()
        //{
        //    return context.PersonGroupPeoples
        //        .OrderBy(p => p.GroupPeoples)
        //        .ToList();
        //}

        //    public List<SelectListItem> GetSelectListGroupPeople()
        //    {
        //        return context.PersonGroupPeoples
        //            .OrderBy(p => p.GroupPeoples)
        //            .Select(s => new SelectListItem
        //            {
        //                Value = s.PersonGroupPeopleID.ToString(),
        //                Text = s.GroupPeoples.GroupName.ToString()),
        //            }).ToList();
        //}

        public PersonGroupPeople FindById(long id)
        {
            return context.PersonGroupPeoples
                .Where(s => s.PersonGroupPeopleID == id)
                .Include(p => p.GroupPeoples)
                .Include(p => p.Person)
                    .ThenInclude(p => p.Department)
                .Single();
        }

        public List<PersonGroupPeople> GetGroupPeoplesOfPerson(long personID)
        {
            return context.PersonGroupPeoples
                .Where(s => s.PersonID == personID)
                .Include(p => p.GroupPeoples)
                .Include(p => p.Person)
                    .ThenInclude(p => p.Department)
                .ToList();
        }

        public List<PersonGroupPeople> GetAllPeopleOfPersonGroupPeople(long groupPeopleID)
        {
            return context.PersonGroupPeoples
                .Where(s => s.GroupPeopleID == groupPeopleID)
                .Include(p => p.GroupPeoples)
                .Include(p => p.Person)
                    .ThenInclude(p => p.Department)
                .ToList();
        }

        public bool PersonGroupPeopleExists(long id)
        {
            return context.PersonGroupPeoples.Any(s => s.PersonGroupPeopleID == id);
        }

        public void Add(PersonGroupPeople personGroupPeople)
        {
            context.PersonGroupPeoples.Add(personGroupPeople);
            context.SaveChanges();
        }

        public void Update(PersonGroupPeople personGroupPeople)
        {
            context.PersonGroupPeoples.Update(personGroupPeople);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var personGroupPeople = context.PersonGroupPeoples.SingleOrDefault(s => s.PersonGroupPeopleID == id);
            context.PersonGroupPeoples.Remove(personGroupPeople);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }


    }
}
