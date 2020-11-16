using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IRoomRepository
    {
        List<Room> GetAllRooms();

        List<SelectListItem> GetSelectListRooms();

        Room FindById(long id);

        bool RoomExists(long id);

        void Update(Room room);

        void Add(Room room);

        void Remove(long id);

        void Save();
    }
}
