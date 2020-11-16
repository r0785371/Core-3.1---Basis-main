using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
   public interface IRackLocationService
    {
        List<RackLocation> GetAllRackLocations();

        List<SelectListItem> GetSelectListLocation();
        List<SelectListItem> GetSelectListRack();

        Tuple<long, RackLocation, List<Asset>> GetAllRackLocationsWithAssets(long rackLocationID);

        RackLocation FindById(long id);

        bool RackLocationExists(long id);

        void Update(RackLocation rackLocation);

        void Add(RackLocation rackLocation);

        void Remove(long id);

        void Save();
    }
}
