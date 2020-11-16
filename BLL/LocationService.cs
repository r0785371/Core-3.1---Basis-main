using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class LocationService: ILocationService
    {
        readonly ILocationRepository repository;
        readonly IRoomRepository repositoryRoom;
        readonly IFloorRepository repositoryFloor;
        readonly IBuildingRepository repositoryBuilding;

        public LocationService(ILocationRepository _repository,
            IRoomRepository _repositoryRoom,IFloorRepository _repositoryFloor,
            IBuildingRepository _repositoryBuilding)
        {
            repository = _repository;
            repositoryRoom = _repositoryRoom;
            repositoryFloor = _repositoryFloor;
            repositoryBuilding = _repositoryBuilding;
        }

        public List<Location> GetAllLocations()
        {
            return repository.GetAllLocations();
        }

        public Location FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool LocationExists(long id)
        {
            return repository.LocationExists(id);
        }

        public void Update(Location location)
        {
            repository.Update(location);
        }

        public void Add(Location location)
        {
            repository.Add(location);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public List<SelectListItem> GetSelectListRooms()
        {
            return repositoryRoom.GetSelectListRooms();
        }

        public List<SelectListItem> GetSelectListFloors()
        {
            return repositoryFloor.GetSelectListFloors();
        }

        public List<SelectListItem> GetSelectListBuildings()
        {
            return repositoryBuilding.GetSelectListBuildings();
        }
    }
}
