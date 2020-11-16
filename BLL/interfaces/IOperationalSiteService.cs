using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IOperationalSiteService
    {
        List<OperationalSite> GetAllOperationalSites();

        OperationalSiteViewModel GetSpecificOperationalSiteAndAllSubForms(long id);

        Tuple<long, OperationalSite, List<Asset>> GetOperationalSiteWithAssets(long operationalSiteID);

        List<SelectListItem> GetSelectListOperationalSiteGroups();

        OperationalSite FindById(long id);

        bool OperationalSiteExists(long id);

        void Update(OperationalSite operationalSite);

        void Add(OperationalSite operationalSite);

        void Remove(long id);

        void Save();
    }
}
