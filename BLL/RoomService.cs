using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RoomService: IRoomService
    {
        readonly IRoomRepository repository;
        readonly ILocationRepository repositoryLocation;

        public RoomService(IRoomRepository _repository, ILocationRepository _repositoryLocation)
        {
            repository = _repository;
            repositoryLocation = _repositoryLocation;
        }

        public List<Room> GetAllRooms()
        {
            return repository.GetAllRooms();
        }

        public Tuple<long, Room, List<Location>> GetAllRoomsWithLocations(long roomID)
        {
            Room room = FindById(roomID);

            List<Location> locations = repositoryLocation.GetAllLocationsOfRoom(roomID);

            return new Tuple<long, Room, List<Location>>(roomID, room, locations);
        }

        public Room FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool RoomExists(long id)
        {
            return repository.RoomExists(id);
        }

        public void Update(Room room)
        {
            repository.Update(room);
        }

        public void Add(Room room)
        {
            repository.Add(room);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
