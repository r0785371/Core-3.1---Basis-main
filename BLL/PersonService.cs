using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class PersonService: IPersonService
    {
        readonly IPersonRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IAssetOwnerRepository repositoryAssetOwner;
        readonly IDepartmentRepository repositoryDepartment;
        readonly IPersonGroupPeopleRepository repositoryPersonGroupPeople;

        //moeten eerst service en repository van PersonGroupPeople aanmaken!!!!!!

        readonly IGroupPeopleRepository repositoryGroupPeople;

        public PersonService(IPersonRepository _repository, IAssetRepository _repositoryAsset,  
            IAssetOwnerRepository _repositoryAssetOwner, IDepartmentRepository _repositoryDepartment, 
            IGroupPeopleRepository _repositoryGroupPeople, IPersonGroupPeopleRepository _repositoryPersonGroupPeople)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryAssetOwner = _repositoryAssetOwner;
            repositoryDepartment = _repositoryDepartment;
            repositoryGroupPeople = _repositoryGroupPeople;
            repositoryPersonGroupPeople = _repositoryPersonGroupPeople;
        }

        public List<Person> GetAllPersons()
        {
            return repository.GetAllPersons();
        }

        public List<SelectListItem> GetSelectListDepartments()
        {
            return repositoryDepartment.GetSelectListDepartments();
        }

        public List<SelectListItem> GetSelectListGroupPeople()
        {
            return repositoryGroupPeople.GetSelectListGroupsPeople();
        }

        public Person FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool PersonExists(long id)
        {
            return repository.PersonExists(id);
        }

        public void Update(Person person)
        {
            repository.Update(person);
        }

        public void Add(Person person)
        {
            //Go to PersonRepository, add the new person and return the (new) PersonID
            repository.Add(person);

            //Go to AssetOwnerRepository and add a new Owner by adding the Person(ID)
            repositoryAssetOwner.AddAssetOwnerPerson(person.PersonID);

            //no need to return anyting, that's why here keep void!

        }

        public Tuple<bool,long, int> Remove(long id)
        {
            bool removedSuccessfull = false;
            int qtyAssets = 0;

            // Before be able to remove a person, need to check first if Person has:
            // one AssetOwner and if there are Assets signed to this AssetOwner. If there are no assets assigned to AssetOwner,
            // can remove the AssetOwner before remove the Person.
            // Person can be linkt also to Backup(s), but this are also linked to Assets. So, if there are no Assets linked to this 
            // person, can also not be any Backup.

            //Will check if exist one AssetOwner for this person, if so it will return the AssetOwnerID, if not it will return 0.
            long assetOwnwerID = repositoryAssetOwner.AssetOwnerExistPerson(id);
            
            if (assetOwnwerID > 0)
            {
                //Will check if there are Assets signed to AssetOwner of this Person.
                qtyAssets = repositoryAsset.QtyAssetsPerAssetOwner(assetOwnwerID);

                //If there are no Assets signed to this Person (AssetOwner) then can remove the AssetOwner.
                if (qtyAssets == 0)
                {
                    repositoryAssetOwner.RemoveAssetOwnerPerson(id);
                    assetOwnwerID = 0;
                }
            }

            //and if no AssetOwner, can remove the Person.
            if (assetOwnwerID==0)
            {
                repository.Remove(id);
                removedSuccessfull = true;
            }

            //  return a message??? If can not remove, why? how many Assets, Backups????

            return new Tuple<bool,long, int>(removedSuccessfull, assetOwnwerID, qtyAssets);
        }

        public List<Asset> GetAllAssetsOfAssetOwner(long assetOwnwerID)
        {
            return repositoryAsset.GetAllAssetsOfAssetOwner(assetOwnwerID);
        }

        public void Save()
        {
            repository.Save();
        }

        public Tuple<long, Person, List<Asset>, List<PersonGroupPeople>> GetPersonWitAssets(long personID)
        {


            Person person = FindById(personID);

            AssetOwner assetOwner = repositoryAssetOwner.GetAssetOwnerOfPerson(personID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfAssetOwner(assetOwner.AssetOwnerID);

            //Then check if person is member of any group(s)
            List<PersonGroupPeople> personGroupPeoples = repositoryPersonGroupPeople.GetGroupPeoplesOfPerson(personID);
            
                //om dan te kunnen checken of van iedere group ook assets aan gelinkt zijn (foreach)
            if (personGroupPeoples != null)
            {
                foreach (var item in personGroupPeoples)
                {
                    AssetOwner assetOwnerGroupPerson = repositoryAssetOwner.GetAssetOwnerOfGroupePeople(item.GroupPeopleID);
                    assets.AddRange(repositoryAsset.GetAllAssetsOfAssetOwner(assetOwnerGroupPerson.AssetOwnerID));
                }
            }

            return new Tuple<long, Person, List<Asset>, List<PersonGroupPeople>>(personID, person, assets, personGroupPeoples);
        }

    }
}
