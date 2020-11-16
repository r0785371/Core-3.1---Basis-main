using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class PersonGroupPeopleService: IPersonGroupPeopleService
    {
        readonly IPersonGroupPeopleRepository repository;
        readonly IGroupPeopleRepository repositoryGroupPeople;
        readonly IPersonRepository repositoryPerson;
        
        public PersonGroupPeopleService(IPersonGroupPeopleRepository _repository, IGroupPeopleRepository _repositoryGroupPeople,
                                        IPersonRepository _repositoryPerson)
        {
            repository = _repository;
            repositoryGroupPeople = _repositoryGroupPeople;
            repositoryPerson = _repositoryPerson;
        }

        public List<PersonGroupPeople> GetAllPersonGroupPeoples()
        {
            return repository.GetAllPersonGroupPeople();
        }

        public List<SelectListItem> GetSelectListGroupPeople()
        {
            return repositoryGroupPeople.GetSelectListGroupsPeople();
        }

        public List<SelectListItem> GetSelectListPerson()
        {
            return repositoryPerson.GetSelectListPeople();
        }

        //public Tuple<long, PersonGroupPeople, List<Location>> GetAllPersonGroupPeoplesWithLocations(long personGroupPeopleID)
        //{
        //    PersonGroupPeople personGroupPeople = FindById(personGroupPeopleID);

        //    List<Location> locations = repositoryLocation.GetAllLocationsOfPersonGroupPeople(personGroupPeopleID);

        //    return new Tuple<long, PersonGroupPeople, List<Location>>(personGroupPeopleID, personGroupPeople, locations);
        //}

        public PersonGroupPeople FindById(long id)
        {
            return repository.FindById(id);
        }

        public List<PersonGroupPeople> GetGroupPeoplesOfPerson(long personID)
        {
            return repository.GetGroupPeoplesOfPerson(personID);
        }

        public List<PersonGroupPeople> GetAllPeopleOfPersonGroupPeople(long groupPeopleID)
        {
            return repository.GetAllPeopleOfPersonGroupPeople(groupPeopleID);
        }

        public bool PersonGroupPeopleExists(long id)
        {
            return repository.PersonGroupPeopleExists(id);
        }

        public void Update(PersonGroupPeople personGroupPeople)
        {
            repository.Update(personGroupPeople);
        }

        public void Add(PersonGroupPeople personGroupPeople)
        {
            repository.Add(personGroupPeople);
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
