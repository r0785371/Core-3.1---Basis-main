using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IAssetRepository
    {
        List<Asset> GetAllAssets();

        IEnumerable<Asset> GetAllAssetsEnum();

        List<Asset> GetAllAssetsOfPuchaseItem(long purchaseItemID);

        List<Asset> GetAllAssetsOfStatus(long statusID);

        List<Asset> GetAllAssetsOfOperationalSiteLocation(long operationalSiteLocationID);

        List<Asset> GetAssetsOfAssetOwner(long id);

        List<Asset> GetAllAssetsOfWarningPeriod(long warningPeriodID);

        List<SelectListItem> GetSelectListAssets();

        List<Asset> GetAllAssetsOfRackLocation(long rackLocationID);

        List<Asset> GetAllAssetsOfUrack(long uRackID);

        string FindMaxTagNo(string tagRef);

        //string FindMaxTagNo(long id);

        Asset FindById(long id);

        bool AssetExists(long id);

        bool TagNumberExists(string tagNumber);

        Asset Update(Asset asset);

        List<Asset> UpdateList(List<Asset> assets);

        void Add(Asset asset);

        List<Asset> AddList(List<Asset> assets);

        void Remove(long id);

        void Save();

        int QtyAssetsPerAssetOwner(long id);

        List<Asset> GetAllAssetsOfAssetOwner(long assetOwnerID);

        List<Asset> GetAllAssetsNeedBackUp();

        IEnumerable<object> GetOperationalSiteGrid();

        int GetAssetQtyPerPurchaseItem(long purchaseItemID);

        //voor chart
        int GetWarrentyItems();
        int GetExpiredItems();

        int GetBackupNeeded();

    }
}
