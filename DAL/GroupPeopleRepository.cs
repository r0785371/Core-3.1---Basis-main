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
    public class GroupPeopleRepository: IGroupPeopleRepository
    {
        readonly DataContext context;

        public GroupPeopleRepository(DataContext _context)
        {
            context = _context;
        }

        public List<GroupPeople> GetAllGroupsPeople()
        {
            return context.GroupsPeople
                .Include(g => g.PersonGroupPeoples)
                .ThenInclude(g => g.Person)
                .ToList();
        }

        public List<SelectListItem> GetSelectListGroupsPeople()
        {
            return context.GroupsPeople.Select(s => new SelectListItem
            {
                Value = s.GroupPeopleID.ToString(),
                Text = s.GroupName,
                //Selected=c.GroupPeopleID.Equals(1)
            }).ToList();
        }

        public List<GroupPeople> GetAllGroupsPeopleOfPerson(long personID)
        {
            return context.GroupsPeople
                .Where(s => s.PersonGroupPeoples.Any(g => g.PersonID == personID))
                .Include(g => g.PersonGroupPeoples)
                    .ThenInclude(g => g.Person)
               .ToList();
        }

        public GroupPeople FindById(long id)
        {
            return context.GroupsPeople
                .Where(s => s.GroupPeopleID == id)
                .Include(g => g.PersonGroupPeoples)
                .ThenInclude(g => g.Person)
                .Single();
        }

        public bool GroupPeopleExists(long id)
        {
            return context.GroupsPeople.Any(s => s.GroupPeopleID == id);
        }

        public void Add(GroupPeople groupPeople)
        {
            context.GroupsPeople.Add(groupPeople);
            context.SaveChanges();
        }

        public void Update(GroupPeople groupPeople)
        {
            context.GroupsPeople.Update(groupPeople);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var groupPeople = context.GroupsPeople.SingleOrDefault(s => s.GroupPeopleID == id);
            context.GroupsPeople.Remove(groupPeople);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
