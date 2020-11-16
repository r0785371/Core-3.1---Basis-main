using DAL;
using BLL;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.interfaces;
using Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.interfaces;

namespace BLL
{
    public class OperationalSiteLocationService : IOperationalSiteLocationService
    {
        readonly IOperationalSiteLocationRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IOperationalSiteRepository repositoryOperationalSite;
        readonly ILocationRepository repositoryLocation;

        public OperationalSiteLocationService(IOperationalSiteLocationRepository _repository,
          IAssetRepository _repositoryAsset, 
          IOperationalSiteRepository _repositoryOperationalSite,
          ILocationRepository _repositoryLocation)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryOperationalSite = _repositoryOperationalSite;
            repositoryLocation = _repositoryLocation;
        }

        public void Add(OperationalSiteLocation operationalSiteLocation)
        {
            repository.Add(operationalSiteLocation);
            
        }

        public OperationalSiteLocation FindById(long id)
        {
            return repository.FindById(id);
        }

        public List<OperationalSiteLocation> GetAllOperationalSiteLocations()
        {
            return repository.GetAllOperationalSiteLocations();
        }

        public Tuple<long, OperationalSiteLocation, List<Asset>> GetOperationalSiteLocationWithSub(long operationalSiteLocationID)
        {
            OperationalSiteLocation operationalSiteLocation = FindById(operationalSiteLocationID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfOperationalSiteLocation(operationalSiteLocationID);

            return new Tuple<long, OperationalSiteLocation, List<Asset>>(operationalSiteLocationID, operationalSiteLocation, assets);
        }

        public List<SelectListItem> GetSelectListLocations()
        {
            return repositoryLocation.GetSelectListLocations();
        }

        public List<SelectListItem> GetSelectListOperationalSite()
        {
            return repositoryOperationalSite.GetSelectListOperationalSites();
        }

        public List<SelectListItem> GetSelectOperationalSites(long? operationalSiteID)
        {
            if (operationalSiteID != null)
            {
                return repositoryOperationalSite.GetSelectOperationalSites(operationalSiteID.Value);
            }
            return repositoryOperationalSite.GetSelectListOperationalSites();
        }

        public bool OperationalSiteLocationExists(long id)
        {
            return repository.OperationalSiteLocationExists(id);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public void Update(OperationalSiteLocation operationalSiteLocation)
        {
            repository.Update(operationalSiteLocation);
        }
    }
}
