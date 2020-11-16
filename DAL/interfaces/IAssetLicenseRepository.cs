using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IAssetLicenseRepository
    {
        List<AssetLicense> GetAllAssetLicenses();

        List<SelectListItem> GetSelectListAssetLicenses();

        AssetLicense FindById(long id);

        List<AssetLicense> GetAllAssetLicensesPerAsset(long id);

        List<License> GetAllLicensesPerAsset(long assetID);

        bool AssetLicenseExists(long id);

        int QtyAssetLicensePerAsset(long id);

        int QtyAssetLicensePerLicense(long licenseID);

        List<Asset> AssetsPerLincense(long licenseID);

        void Add(AssetLicense assetLicense);

        void AddListAssetLicenses(List<AssetLicense> assetLicenses);

        void Update(AssetLicense assetLicense);

        void Remove(long id);

        void Save();
    }
}
