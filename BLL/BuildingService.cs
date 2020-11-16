using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BuildingService: IBuildingService
    {
        readonly IBuildingRepository repository;
        readonly ILocationRepository repositoryLocation;

        public BuildingService(IBuildingRepository _repository, ILocationRepository _repositoryLocation)
        {
            repository = _repository;
            repositoryLocation = _repositoryLocation;
        }

        public List<Building> GetAllBuildings()
        {
            return repository.GetAllBuildings();
        }

        public Tuple<long, Building, List<Location>> GetAllBuildingsWithLocations(long buildingID)
        {
            Building building = FindById(buildingID);

            List<Location> locations = repositoryLocation.GetAllLocationsOfBuilding(buildingID);

            return new Tuple<long, Building, List<Location>>(buildingID, building, locations);
        }

        public Building FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool BuildingExists(long id)
        {
            return repository.BuildingExists(id);
        }

        public void Update(Building building)
        {
            repository.Update(building);
        }

        public void Add(Building building)
        {
            repository.Add(building);
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
