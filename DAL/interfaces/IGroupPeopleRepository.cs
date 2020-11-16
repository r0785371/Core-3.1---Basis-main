using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IGroupPeopleRepository
    {
        List<GroupPeople> GetAllGroupsPeople();

        List<SelectListItem> GetSelectListGroupsPeople();

        List<GroupPeople> GetAllGroupsPeopleOfPerson(long personID);

        GroupPeople FindById(long id);

        bool GroupPeopleExists(long id);

        void Update(GroupPeople groupPeople);

        void Add(GroupPeople groupPeople);

        void Remove(long id);

        void Save();
    }
}
