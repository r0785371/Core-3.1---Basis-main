using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AssetHistoryService : IAssetHistoryService
    {
        readonly IAssetHistoryRepository repository;
        readonly IAssetRepository repositoryAsset;

        public AssetHistoryService(IAssetHistoryRepository _repository, IAssetRepository _repositoryAsset)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
        }

        public List<AssetHistory> GetAllAssetHistories()
        {
            return repository.GetAllAssetHistories();
        }

        //public List<SelectListItem> GetSelectListAssetHistories()
        //{
        //    return repository.GetSelectListAssetHistories();
        //}

        public AssetHistory FindById(long id)
        {
            return repository.FindById(id);
        }

        public Tuple<long, AssetHistory, Asset> GetAssetHistoryWithSub(long assetHistoryID)
        {
            AssetHistory assetHistory = FindById(assetHistoryID);

            Asset asset = repositoryAsset.FindById(assetHistory.AssetID);

            return new Tuple<long, AssetHistory, Asset>(assetHistoryID, assetHistory, asset);
        }

        public bool AssetHistoryExists(long id)
        {
            return repository.AssetHistoryExists(id);
        }

        public void Update(AssetHistory assetHistory)
        {
            repository.Update(assetHistory);
        }

        public void Add(AssetHistory assetHistory)
        {
            repository.Add(assetHistory);
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
