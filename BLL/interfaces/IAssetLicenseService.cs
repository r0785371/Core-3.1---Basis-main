using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IAssetLicenseService
    {

        List<AssetLicense> GetAllAssetLicenses();

        List<AssetLicense> GetAllAssetLicensesPerAsset(long id);

        List<SelectListItem> GetSelectListAssets();

        List<SelectListItem> GetSelectListLicenses();

        AssetLicense FindById(long id);

        bool AssetLicenseExists(long id);

        void Update(AssetLicense assetLicense);

        void Add(AssetLicense assetLicense);

        void Remove(long id);

        void Save();
    }
}
