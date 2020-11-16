using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IFloorRepository
    {
        List<Floor> GetAllFloors();

        List<SelectListItem> GetSelectListFloors();

        Floor FindById(long id);

        bool FloorExists(long id);

        void Update(Floor floor);

        void Add(Floor floor);

        void Remove(long id);

        void Save();
    }
}
