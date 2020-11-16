using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
     public interface IAssetOwnerRepository
    {
        List<AssetOwner> GetAllAssetOwners();

        List<SelectListItem> GetSelectListAssetOwners();

        AssetOwner GetAssetOwnerOfPerson(long personID);

        AssetOwner GetAssetOwnerOfOperationalSite(long operationalSiteID);

        AssetOwner GetAssetOwnerOfGroupePeople(long groupPeopleID);

        AssetOwner GetAssetOwnerOfWarehouse(long warehouseID);

        AssetOwner GetAssetOwnerOfExterCompany(long externCompanyID);

        AssetOwner FindById(long id);

        bool AssetOwnerExists(long id);

        void Update(AssetOwner assetOwner);

        void Add(AssetOwner assetOwner);

        void Remove(long id);

        void Save();

        void AddAssetOwnerGroupPeople(long id);

        void RemoveAssetOwnerGroupPeople(long id);

        void AddAssetOwnerExternCompany(long id);

        void RemoveAssetOwnerExternCompany(long id);

        void AddAssetOwnerOperationalSite(long id);

        long AssetOwnerExistOperationalSite(long id);

        void RemoveAssetOwnerOperationalSite(long id);

        void AddAssetOwnerPerson(long id);

        long AssetOwnerExistPerson(long id);

        void RemoveAssetOwnerPerson(long id);

        void AddAssetOwnerWarehouse(long id);

        void RemoveAssetOwnerWarehouse(long id);
    }
}
