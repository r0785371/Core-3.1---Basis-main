using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IAssetHistoryService
    {
        List<AssetHistory> GetAllAssetHistories();

        //List<SelectListItem> GetSelectListAssetHistories();

        AssetHistory FindById(long id);

        Tuple<long, AssetHistory, Asset>GetAssetHistoryWithSub(long assetHistoryID);

        bool AssetHistoryExists(long id);

        void Update(AssetHistory assetHistory);

        void Add(AssetHistory assetHistory);

        void Remove(long id);

        void Save();
    }
}
