using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IAssetHistoryRepository
    {
        List<AssetHistory> GetAllAssetHistories();

        //List<SelectListItem> GetSelectListAssetHistories();

        AssetHistory FindById(long id);

        List<AssetHistory> GetAllAssetHistoriesOfAsset(long assetId);

        AssetHistory GetLatestAssetHistoryOfAsset(long assetID);

        bool AssetHistoryExists(long id);

        void Update(AssetHistory assetHistory);

        void Add(AssetHistory assetHistory);

        void Remove(long id);

        void Save();
    }
}
