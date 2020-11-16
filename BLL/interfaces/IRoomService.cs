using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IRoomService
    {
        List<Room> GetAllRooms();

        Tuple<long, Room, List<Location>> GetAllRoomsWithLocations(long roomID);

        Room FindById(long id);

        bool RoomExists(long id);

        void Update(Room room);

        void Add(Room room);

        void Remove(long id);

        void Save();
    }
}
