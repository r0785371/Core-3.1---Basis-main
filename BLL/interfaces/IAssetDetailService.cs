using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IAssetDetailService
    {
        List<AssetDetail> GetAllAssetDetails();

        List<SelectListItem> GetSelectListDetails();

        List<SelectListItem>GetSelectListDetailsOfProductType(long assetID);

        AssetDetail FindById(long id);

        bool AssetDetailExists(long id);

        void Update(AssetDetail assetDetail);

        void Add(AssetDetail assetDetail);

        void Remove(long id);

        void Save();
    }
}
