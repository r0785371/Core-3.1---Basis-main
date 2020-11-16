using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RackLocationService: IRackLocationService
    {
        readonly IRackLocationRepository repository;
        readonly IRackRepository repositoryRack;
        readonly ILocationRepository repositoryLocation;
        readonly IAssetRepository assetRepository;

        public RackLocationService(IRackLocationRepository _repository,
            IRackRepository _repositoryRack, ILocationRepository _repositoryLocation, IAssetRepository _assetRepository)
        {
            repository = _repository;
            repositoryRack = _repositoryRack;
            repositoryLocation = _repositoryLocation;
            assetRepository = _assetRepository;
        }

        public List<RackLocation> GetAllRackLocations()
        {
            return repository.GetAllRackLocations();
        }

        public RackLocation FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool RackLocationExists(long id)
        {
            return repository.RackLocationExists(id);
        }

        public void Update(RackLocation rackLocation)
        {
            repository.Update(rackLocation);
        }

        public void Add(RackLocation rackLocation)
        {
            repository.Add(rackLocation);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public List<SelectListItem> GetSelectListLocation()
        {
            return repositoryLocation.GetSelectListLocations();
        }

        public List<SelectListItem> GetSelectListRack()
        {
            return repositoryRack.GetSelectListRacks();
        }

        public Tuple<long, RackLocation, List<Asset>> GetAllRackLocationsWithAssets(long rackLocationID)
        {
            RackLocation rackLocation = FindById(rackLocationID);

            List<Asset> assets = assetRepository.GetAllAssetsOfRackLocation(rackLocationID);

            return new Tuple<long, RackLocation, List<Asset>>(rackLocationID, rackLocation, assets);
        }
    }
}
