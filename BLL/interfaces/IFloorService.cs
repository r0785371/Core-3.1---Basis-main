using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
   public interface IFloorService
    {
        List<Floor> GetAllFloors();

        Tuple<long, Floor, List<Location>> GetAllFloorsWithLocations(long floorID);

        Floor FindById(long id);

        bool FloorExists(long id);

        void Update(Floor floor);

        void Add(Floor floor);

        void Remove(long id);

        void Save();
    }
}
