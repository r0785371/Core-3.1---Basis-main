using BLL.interfaces;
using BLL.ViewModels;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class OperationalSiteService: IOperationalSiteService
    {
        readonly IOperationalSiteRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IAssetOwnerRepository repositoryAssetOwner;
        readonly IOperationalSiteLocationRepository repositoryOperationalSiteLocation;

        public OperationalSiteService(IOperationalSiteRepository _repository, IAssetRepository _repositoryAsset,
            IAssetOwnerRepository _repositoryAssetOwner,
            IOperationalSiteLocationRepository _repositoryOperationalSiteLocation)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryAssetOwner = _repositoryAssetOwner;
            repositoryOperationalSiteLocation = _repositoryOperationalSiteLocation;
        }

        public List<OperationalSite> GetAllOperationalSites()
        {
            return repository.GetAllOperationalSites();
        }

        public OperationalSiteViewModel GetSpecificOperationalSiteAndAllSubForms(long id)
        {
            OperationalSiteViewModel operationalSiteViewModel = new OperationalSiteViewModel();

            //Get the specific OperationalSite info....
            operationalSiteViewModel.operationalSite = FindById(id);
            operationalSiteViewModel.operationalSiteLocations = repositoryOperationalSiteLocation.GetAllOperationalSiteLocationsPerOS(id, operationalSiteViewModel.operationalSite.OperationalSiteGroupId);
            operationalSiteViewModel.assetOwner.AssetOwnerID = repositoryAssetOwner.AssetOwnerExistOperationalSite(id);
            operationalSiteViewModel.assets = repositoryAsset.GetAssetsOfAssetOwner(operationalSiteViewModel.assetOwner.AssetOwnerID);

            if (operationalSiteViewModel.operationalSite.OperationalSiteGroupId != null)
            {
                long assetOwnerIDOperationalSiteGroup = repositoryAssetOwner.AssetOwnerExistOperationalSite(operationalSiteViewModel.operationalSite.OperationalSiteGroupId.Value);
                operationalSiteViewModel.assets.AddRange(repositoryAsset.GetAllAssetsOfAssetOwner(assetOwnerIDOperationalSiteGroup));
            }

            return operationalSiteViewModel;
        }

        public Tuple<long, OperationalSite, List<Asset>> GetOperationalSiteWithAssets(long operationalSiteID)
        {
            OperationalSite operationalSite = FindById(operationalSiteID);

            AssetOwner assetOwner = repositoryAssetOwner.GetAssetOwnerOfOperationalSite(operationalSiteID);

            List<Asset> assets = repositoryAsset.GetAssetsOfAssetOwner(assetOwner.AssetOwnerID);

            if (operationalSite.OperationalSiteGroupId != null)
            {
                AssetOwner assetOwnerGroup = repositoryAssetOwner.GetAssetOwnerOfOperationalSite(operationalSite.OperationalSiteGroupId.Value);
                assets.AddRange(repositoryAsset.GetAssetsOfAssetOwner(assetOwnerGroup.AssetOwnerID));
            }

            return new Tuple<long, OperationalSite, List<Asset>>(operationalSiteID, operationalSite, assets);
        }

        public List<SelectListItem> GetSelectListOperationalSiteGroups()
        {
            return repository.GetSelectListOperationalSiteGroups();
        }

        public OperationalSite FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool OperationalSiteExists(long id)
        {
            return repository.OperationalSiteExists(id);
        }

        public void Update(OperationalSite operationalSite)
        {
            repository.Update(operationalSite);
        }

        public void Add(OperationalSite operationalSite)
        {
            //Go to OperationalSiteRepository, add the new operationalSite and return the (new) OperationalSiteID
            repository.Add(operationalSite);

            //Go to AssetOwnerRepository and add a new Owner by adding the OperationalSite(ID)
            repositoryAssetOwner.AddAssetOwnerOperationalSite(operationalSite.OperationalSiteID);

            //no need to return anyting, that's why here keep void!
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
