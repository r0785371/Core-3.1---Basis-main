using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AssetOwnerService: IAssetOwnerService
    {
        readonly IAssetOwnerRepository repository;

        public AssetOwnerService(IAssetOwnerRepository _repository)
        {
            repository = _repository;
        }

        public List<AssetOwner> GetAllAssetOwners()
        {
            return repository.GetAllAssetOwners();
        }

        public AssetOwner FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool AssetOwnerExists(long id)
        {
            return repository.AssetOwnerExists(id);
        }

        public void Update(AssetOwner assetOwner)
        {
            repository.Update(assetOwner);
        }

        public void Add(AssetOwner assetOwner)
        {
            repository.Add(assetOwner);
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
