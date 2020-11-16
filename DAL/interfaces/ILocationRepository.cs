using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAllLocations();

        List<SelectListItem> GetSelectListLocations();

        List<SelectListItem> GetListLocationsIsWarehouse();

        List<Location> GetAllLocationsOfBuilding(long buildingID);

        List<Location> GetAllLocationsOfFloor(long floorID);

        List<Location> GetAllLocationsOfRoom(long roomID);

        Location FindById(long id);

        bool LocationExists(long id);

        void Update(Location Location);

        void Add(Location Location);

        void Remove(long id);

        void Save();
    }
}
