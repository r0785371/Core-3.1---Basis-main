using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface ILocationService
    {
        List<Location> GetAllLocations();

        List<SelectListItem> GetSelectListRooms();

        List<SelectListItem> GetSelectListFloors();

        List<SelectListItem> GetSelectListBuildings();

        Location FindById(long id);

        bool LocationExists(long id);

        void Update(Location loacation);

        void Add(Location loacationl);

        void Remove(long id);

        void Save();
    }
}
