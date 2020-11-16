using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IAssetDetailRepository
    {
        List<AssetDetail> GetAllAssetDetails();

        List<SelectListItem> GetSelectListAssetDetails();

        List<AssetDetail> GetAllAssetDetailsOfDetail(long detailID);

        AssetDetail FindById(long id);

        bool AssetDetailExists(long id);

        void Update(AssetDetail assetDetail);

        void Add(AssetDetail assetDetail);

        void Remove(long id);

        void Save();

        List<AssetDetail> GetAllAssetDetailsPerAsset(long id);
    }
}
