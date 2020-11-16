using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IPersonGroupPeopleService
    {
        List<PersonGroupPeople> GetAllPersonGroupPeoples();

        //Tuple<long, PersonGroupPeople, List<Location>> GetAllPersonGroupPeoplesWithLocations(long personGroupPeopleID);

        List<SelectListItem> GetSelectListGroupPeople();

        List<SelectListItem> GetSelectListPerson();

        PersonGroupPeople FindById(long id);

        List<PersonGroupPeople> GetGroupPeoplesOfPerson(long personID);

        bool PersonGroupPeopleExists(long id);

        void Update(PersonGroupPeople personGroupPeople);

        void Add(PersonGroupPeople personGroupPeople);

        void Remove(long id);

        void Save();
    }
}
