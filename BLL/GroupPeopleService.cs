using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class GroupPeopleService: IGroupPeopleService
    {
        readonly IGroupPeopleRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IAssetOwnerRepository repositoryAssetOwner;
        readonly IPersonRepository repositoryPerson;
        readonly IPersonGroupPeopleRepository repositoryPersonGroupPeople;

        public GroupPeopleService(IGroupPeopleRepository _repository, IAssetRepository _repositoryAsset,
            IAssetOwnerRepository _repositoryAssetOwner, IPersonRepository _repositoryPerson, IPersonGroupPeopleRepository _repositoryPersonGroupPeople)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryAssetOwner = _repositoryAssetOwner;
            repositoryPerson = _repositoryPerson;
            repositoryPersonGroupPeople = _repositoryPersonGroupPeople;
        }

        public List<GroupPeople> GetAllGroupsPeople()
        {
            return repository.GetAllGroupsPeople();
        }

        public List<SelectListItem> GetSelectListPeople()
        {
            return repositoryPerson.GetSelectListPeople();
        }

        public Tuple<long, GroupPeople, List<PersonGroupPeople>, List<Asset>> GetGroupPeopleWithSub(long groupPeopleID)
        {
            GroupPeople groupPeople = FindById(groupPeopleID);

            List<PersonGroupPeople> personGroupPeoples = repositoryPersonGroupPeople.GetAllPeopleOfPersonGroupPeople(groupPeopleID);

            AssetOwner assetOwner = repositoryAssetOwner.GetAssetOwnerOfGroupePeople(groupPeopleID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfAssetOwner(assetOwner.AssetOwnerID);

            return new Tuple<long, GroupPeople, List<PersonGroupPeople>, List<Asset>>(groupPeopleID, groupPeople, personGroupPeoples, assets);
        }

        public GroupPeople FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool GroupPeopleExists(long id)
        {
            return repository.GroupPeopleExists(id);
        }

        public void Update(GroupPeople groupPeople)
        {
            repository.Update(groupPeople);
        }

        public void Add(GroupPeople groupPeople)
        {
            //Go to GroupPeopleRepository, add the new groupPeople and return the (new) GroupPeopleID
            repository.Add(groupPeople);

            //Go to AssetOwnerRepository and add a new Owner by adding the GroupPeople(ID)
            repositoryAssetOwner.AddAssetOwnerGroupPeople(groupPeople.GroupPeopleID);

            //no need to return anyting, that's why here keep void!
        }

        public void Remove(long id)
        {
            repositoryAssetOwner.RemoveAssetOwnerGroupPeople(id);
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
