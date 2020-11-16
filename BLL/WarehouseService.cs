using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
   public class WarehouseService: IWarehouseService
    {
        readonly IWarehouseRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IAssetOwnerRepository repositoryAssetOwner;
        readonly ILocationRepository repositoryLocation;

        public WarehouseService(IWarehouseRepository _repository, IAssetRepository _repositoryAsset, 
            IAssetOwnerRepository _repositoryAssetOwner, ILocationRepository _repositoryLocation)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryAssetOwner = _repositoryAssetOwner;
            repositoryLocation = _repositoryLocation;
        }

        public List<Warehouse> GetAllWarehouses()
        {
            return repository.GetAllWarehouses();
        }

        public List<SelectListItem> GetListLocationsIsWarehouse()
        {
            return repositoryLocation.GetListLocationsIsWarehouse();
        }

        public Tuple<long, Warehouse, List<Asset>> GetWarehouseWithAssets(long warehouseID)
        {
            Warehouse warehouse = FindById(warehouseID);

            AssetOwner assetOwner = repositoryAssetOwner.GetAssetOwnerOfWarehouse(warehouseID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfAssetOwner(assetOwner.AssetOwnerID);

            return new Tuple<long, Warehouse, List<Asset>>(warehouseID, warehouse, assets);
        }

        public Warehouse FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool WarehouseExists(long id)
        {
            return repository.WarehouseExists(id);
        }

        public void Update(Warehouse warehouse)
        {
            repository.Update(warehouse);
        }

        public void Add(Warehouse warehouse)
        {
            repository.Add(warehouse);

            //Go to AssetOwnerRepository and add a new Owner by adding the Person(ID)
            repositoryAssetOwner.AddAssetOwnerWarehouse(warehouse.WarehouseID);

            //no need to return anyting, that's why here keep void!
        }

        public void Remove(long id)
        {
            repositoryAssetOwner.RemoveAssetOwnerWarehouse(id);
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
