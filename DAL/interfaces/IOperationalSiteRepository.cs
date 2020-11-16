using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IOperationalSiteRepository
    {
        List<OperationalSite> GetAllOperationalSites();

        List<SelectListItem> GetSelectListOperationalSites();

        List<SelectListItem> GetSelectOperationalSites(long operationalSiteID);

        List<SelectListItem> GetSelectListOperationalSiteGroups();

        OperationalSite FindById(long id);

        bool OperationalSiteExists(long id);

        void Update(OperationalSite operationalSite);

        long Add(OperationalSite operationalSite);

        void Remove(long id);

        void Save();
    }
}
