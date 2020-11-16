using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class FloorService: IFloorService
    {
        readonly IFloorRepository repository;
        readonly ILocationRepository repositoryLocation;

        public FloorService(IFloorRepository _repository, ILocationRepository _repositoryLocation)
        {
            repository = _repository;
            repositoryLocation = _repositoryLocation;
        }

        public List<Floor> GetAllFloors()
        {
            return repository.GetAllFloors();
        }

        public Tuple<long, Floor, List<Location>> GetAllFloorsWithLocations(long floorID)
        {
            Floor floor = FindById(floorID);

            List<Location> locations = repositoryLocation.GetAllLocationsOfFloor(floorID);

            return new Tuple<long, Floor, List<Location>>(floorID, floor, locations);
        }

        public Floor FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool FloorExists(long id)
        {
            return repository.FloorExists(id);
        }

        public void Update(Floor floor)
        {
            repository.Update(floor);
        }

        public void Add(Floor floor)
        {
            repository.Add(floor);
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
