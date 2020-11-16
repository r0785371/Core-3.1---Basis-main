using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IGroupPeopleService
    {
        List<GroupPeople> GetAllGroupsPeople();

        List<SelectListItem> GetSelectListPeople();

        Tuple<long, GroupPeople, List<PersonGroupPeople>, List<Asset>> GetGroupPeopleWithSub(long groupPeopleID);

        GroupPeople FindById(long id);

        bool GroupPeopleExists(long id);

        void Update(GroupPeople groupPeople);

        void Add(GroupPeople groupPeople);

        void Remove(long id);

        void Save();
    }
}
