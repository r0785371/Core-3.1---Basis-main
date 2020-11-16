using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IBuildingRepository
    {
        List<Building> GetAllBuildings();

        List<SelectListItem> GetSelectListBuildings();

        Building FindById(long id);

        bool BuildingExists(long id);

        void Update(Building building);

        void Add(Building building);

        void Remove(long id);

        void Save();
    }
}
