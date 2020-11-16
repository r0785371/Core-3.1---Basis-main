using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.interfaces
{
    public interface IAssetService
    {
        List<Asset> GetAllAssets();

        IEnumerable<Asset> GetAllAssetsEnum(string searchString, bool outSwitch);

        IEnumerable<object> GetOperationalSiteGrid();

        Tuple<long, Asset, List<Backup>, List<AssetDetail>, List<License>> GetAssetWithSubs(long assetID);

        List<Asset> GetAllAssetsOfPuchaseItem(long purchaseItemID);

        List<SelectListItem> GetSelectListAssetOwners();

        List<SelectListItem> GetSelectListStatusAsset();

        List<SelectListItem> GetSelectListOperationalSiteLocations();

        List<SelectListItem> GetSelectListOperationalSiteLocationsByAssetOwnwer(long assetOwnerID);

        List<OperationalSiteLocation> GetOperationalSiteLocationsByOwner(long ownerID);

        List<RackLocation> GetRackLocationsIDByOperationalSiteLocation(long operationalSiteLocationID);

        List<SelectListItem> GetSelectListPurchaseItems();

        List<SelectListItem> GetSelectListRackLocations();

        List<SelectListItem> GetSelectListRackLocationsByOperationalSiteLocation(long? operationalSiteLocationID);

        List<SelectListItem> GetSelectListRacks();

        List<SelectListItem> GetSelectListURacks();

        List<SelectListItem> GetSelectListWarningPeriods();

        List<Asset> CreateAssetsWithPurchaseItem(long purchaseItemID, int qtyAdd);

        Asset FindById(long id);

        bool AssetExists(long id);

        Tuple<long, string, bool, string> CheckIfTagNumberExists(long purchaseItemID, int gotTagNumber);

        Tuple<Asset, int> CreateNewAsset(long purchaseItemID, string assetTag);

        void Update(Asset asset);

        List<Asset> UpdateList(List<Asset> assets);

        List<Asset> AddList(List<Asset> assets);

        Tuple<Asset, bool, int, string> Add(Asset asset);

        void Remove(long id);

        void Save();

        AssetViewModel GetSpecificAssetAndAllSubForms(long id);

        List<Asset> GetAllAssetsNeedBackUp();

        AssetCounterViewModel GetAssetCounterViewModel();

        List<SelectListItem> GetSelectListOperationalSiteLocationsByAssetOwner(long assetOwnerID);

        List<ListAssetsPivotData> GetAllAssetsPivotData();

        AssetOwner GetAssetOwner(long assetOwnerID);

    }
}
