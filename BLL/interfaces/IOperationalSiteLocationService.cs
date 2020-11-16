using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IOperationalSiteLocationService
    {
        List<OperationalSiteLocation> GetAllOperationalSiteLocations();

        Tuple<long, OperationalSiteLocation, List<Asset>> GetOperationalSiteLocationWithSub(long operationalSiteLocationID);

        List<SelectListItem> GetSelectListLocations();

        List<SelectListItem> GetSelectListOperationalSite();

        List<SelectListItem> GetSelectOperationalSites(long? operationalSiteID);

        OperationalSiteLocation FindById(long id);

        bool OperationalSiteLocationExists(long id);

        void Update(OperationalSiteLocation operationalSiteLocation);

        void Add(OperationalSiteLocation operationalSiteLocation);

        void Remove(long id);

        void Save();





    }
}
