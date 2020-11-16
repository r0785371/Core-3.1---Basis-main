using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IBuildingService
    {
        List<Building> GetAllBuildings();

        Tuple<long, Building, List<Location>> GetAllBuildingsWithLocations(long buildingID);

        Building FindById(long id);

        bool BuildingExists(long id);

        void Update(Building building);

        void Add(Building building);

        void Remove(long id);

        void Save();
    }
}
