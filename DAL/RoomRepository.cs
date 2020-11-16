using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class RoomRepository: IRoomRepository
    {
        readonly DataContext context;

        public RoomRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Room> GetAllRooms()
        {
            return context.Rooms
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListRooms()
        {
            return context.Rooms
                .OrderBy(o => o.Name)
                .Select(s => new SelectListItem
            {
                Value = s.RoomID.ToString(),
                Text = s.Name,
            }).ToList();
        }

        public Room FindById(long id)
        {
            return context.Rooms.Where(s => s.RoomID == id).Single();
        }

        public bool RoomExists(long id)
        {
            return context.Rooms.Any(s => s.RoomID == id);
        }

        public void Add(Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public void Update(Room room)
        {
            context.Rooms.Update(room);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var room = context.Rooms.SingleOrDefault(s => s.RoomID == id);
            context.Rooms.Remove(room);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
