using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IOperationalSiteLocationRepository
    {
        List<OperationalSiteLocation> GetAllOperationalSiteLocations();

        List<OperationalSiteLocation> GetAllOperationalSiteLocationsPerOS(long id, long? operationalSiteGroupId);

        List<SelectListItem> GetSelectListOperationalSiteLocationsByAssetOwner(long operationalSiteID);

        List<SelectListItem> GetSelectListOperationalSiteLocations();

        List<OperationalSiteLocation> GetOperationalSiteLocationsByOwner(long OperationalSiteId);

        OperationalSiteLocation FindById(long id);

        bool OperationalSiteLocationExists(long id);

        void Update(OperationalSiteLocation operationalSiteLocation);

        void Add(OperationalSiteLocation operationalSiteLocation);

        void Remove(long id);

        void Save();
    }
}
