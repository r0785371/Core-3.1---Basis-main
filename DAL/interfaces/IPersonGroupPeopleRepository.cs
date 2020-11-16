using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IPersonGroupPeopleRepository
    {
        List<PersonGroupPeople> GetAllPersonGroupPeople();

        //List<SelectListItem> GetSelectListGroupPeople();

        PersonGroupPeople FindById(long id);

        List<PersonGroupPeople> GetGroupPeoplesOfPerson(long personID);

        List<PersonGroupPeople> GetAllPeopleOfPersonGroupPeople(long groupPeopleID);

        bool PersonGroupPeopleExists(long id);

        void Update(PersonGroupPeople personGroupPeople);

        void Add(PersonGroupPeople personGroupPeople);

        void Remove(long id);

        void Save();
    }
}
