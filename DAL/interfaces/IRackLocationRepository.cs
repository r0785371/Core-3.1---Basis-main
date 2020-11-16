using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IRackLocationRepository
    {
        List<RackLocation> GetAllRackLocations();

        List<SelectListItem> GetSelectListRackLocations();

        List<SelectListItem> GetSelectListRackLocationsByOperationalSiteLocation(long locationID);

        List<RackLocation> GetAllRackLocationsOfRack(long rackID);

        List<RackLocation> GetRackLocationsByLocation(long locationID);

        RackLocation FindById(long id);

        bool RackLocationExists(long id);

        void Update(RackLocation rackLocation);

        void Add(RackLocation rackLocation);

        void Remove(long id);

        void Save();
    }
}
