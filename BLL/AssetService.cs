using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using BLL.interfaces;
using DAL.interfaces;
using BLL.ViewModels;
using System.Drawing;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using QRCoder;
using NinjaNye.SearchExtensions;

namespace BLL
{
    public class AssetService : IAssetService
    {
        readonly IAssetRepository repository;
        readonly IAssetDetailRepository repositoryAssetDetail;
        readonly IAssetHistoryRepository repositoryAssetHistory;
        readonly IAssetLicenseRepository repositoryAssetLicense;
        readonly IAssetOwnerRepository repositoryAssetOwner;
        readonly IStatusRepository repositoryStatus;
        readonly IBackupRepository repositoryBackup;
        readonly ILicenseRepository repositoryLicense;
        readonly IOperationalSiteRepository repositoryOperationalSite;
        readonly IOperationalSiteLocationRepository repositoryOperationalSiteLocation;
        readonly IPurchaseItemRepository repositoryPurchaseItem;
        readonly IRackRepository repositoryRack;
        readonly IRackLocationRepository repositoryRackLocation;
        readonly IURackRepository repositoryURack;
        readonly IWarningPeriodRepository repositoryWarningPeriod;
        readonly IPurchaseTypeRepository repositoryPurchaseType;
        readonly IWarehouseRepository repositoryWarehouse;
        readonly UserManager<ApplicationUser> userManager;

        public AssetService(IAssetRepository _repository, IAssetDetailRepository _repositoryAssetDetail, IAssetHistoryRepository _repositoryAssetHistory,
            IAssetLicenseRepository _repositoryAssetLicense, IAssetOwnerRepository _repositoryAssetOwner,
            IStatusRepository _repositoryStatus, IBackupRepository _repositoryBackup, ILicenseRepository _repositoryLicense,
            IOperationalSiteRepository _repositoryOperationalSite, IOperationalSiteLocationRepository _repositoryOperationalSiteLocation,
            IPurchaseItemRepository _repositoryPurchaseItem, IRackRepository _repositoryRack, IRackLocationRepository _repositoryRackLocation,
            IURackRepository _repositoryURack, IWarningPeriodRepository _repositoryWarningPeriod, IPurchaseTypeRepository _repositoryPurchaseType,
            IWarehouseRepository _repositoryWarehouse, UserManager<ApplicationUser> userManager)
        {
            repository = _repository;
            repositoryAssetDetail = _repositoryAssetDetail;
            repositoryAssetHistory = _repositoryAssetHistory;
            repositoryAssetLicense = _repositoryAssetLicense;
            repositoryAssetOwner = _repositoryAssetOwner;
            repositoryStatus = _repositoryStatus;
            repositoryBackup = _repositoryBackup;
            repositoryLicense = _repositoryLicense;
            repositoryOperationalSite = _repositoryOperationalSite;
            repositoryOperationalSiteLocation = _repositoryOperationalSiteLocation;
            repositoryPurchaseItem = _repositoryPurchaseItem;
            repositoryRack = _repositoryRack;
            repositoryRackLocation = _repositoryRackLocation;
            repositoryURack = _repositoryURack;
            repositoryWarningPeriod = _repositoryWarningPeriod;
            repositoryPurchaseType = _repositoryPurchaseType;
            repositoryWarehouse = _repositoryWarehouse;
            this.userManager = userManager;
        }

        public List<Asset> GetAllAssets()
        {
            //IEnumerable<object> test = GetOperationalSiteGrid();
            return repository.GetAllAssets();
        }

        public List<ListAssetsPivotData> GetAllAssetsPivotData()
        {
            //List<Asset> assets = GetAllAssets();

            IEnumerable<Asset> assets = GetAllAssetsEnum("", false);

            var assetList = assets
                .GroupBy(a => new {

                    //OwnerType = a.AssetOwner.ExternCompanyId != null ? "Extern company" : a.AssetOwner.GroupPeopleId != null ? "Group people" :
                    //            a.AssetOwner.OperationalSiteId != null ? "Operational site" : a.AssetOwner.PersonId != null ? "Person" :
                    //            a.AssetOwner.WarehouseId != null ? "Warehouse" : "",

                    a.AssetOwner.OwnerType,

                    a.AssetOwner.OwnerDescription, 

                    a.PurchaseItem.Product.ProductType.Name, 
                    Product = a.PurchaseItem.Product.Name, 
                    a.DeliveryDate, 
                    a.ExpireDate, 
                    a.WarningDate, 
                    a.WarningUrgency, 
                    LocationDescription = a.OperationalSiteLocation != null ? a.OperationalSiteLocation.Location != null ? a.OperationalSiteLocation.Location.LocationDescription ?? "" : "" : "", 
                    RackNo = a.RackLocation != null ? a.RackLocation.RackNo ?? "" : "", 
                    RackName = a.RackLocation != null ? a.RackLocation.Rack.Name ?? "" : "", 
                    URack = a.URack != null ? a.URack.Name ?? "" : "", 
                    a.PurchaseItem.Price })
                .Select(g => new ListAssetsPivotData
                {
                    ProductType = g.Key.Name,
                    ProductName = g.Key.Product.ToString(),

                    OwnerType = g.Key.OwnerType,

                    Owner = g.Key.OwnerDescription,
                    DeliveryDateYear = g.Key.DeliveryDate.Year.ToString(),
                    DeliveryDateMonth = g.Key.DeliveryDate.Month.ToString(),
                    ExpireDateMonth = g.Key.ExpireDate.Month.ToString(),
                    ExpireDateYear = g.Key.ExpireDate.Year.ToString(),
                    WarningPeriodMonth = g.Key.WarningDate.Month.ToString(),
                    WarningPeriodYear = g.Key.WarningDate.Year.ToString(),
                    WarningUrgency = g.Key.WarningUrgency.ToString(),
                    Location = g.Key.LocationDescription.ToString(),
                    Rack = g.Key.RackNo.ToString() + " - " + g.Key.RackName.ToString(),
                    URack = g.Key.URack.ToString(),
                    Price = g.Key.Price,

                })
                .ToList();

            return assetList;


            //return repository.GetAllAssetsPivotData();
        }

        public List<Asset> GetAllAssetsOfPuchaseItem(long purchaseItemID)
        {
            return repository.GetAllAssetsOfPuchaseItem(purchaseItemID);
        }

        public List<SelectListItem> GetSelectListAssetOwners()
        {
            return repositoryAssetOwner.GetSelectListAssetOwners();
        }

        public List<SelectListItem> GetSelectListStatusAsset()
        {
            return repositoryStatus.GetSelectListStatusAsset();
        }

        public List<SelectListItem> GetSelectListOperationalSiteLocations()
        {
            return repositoryOperationalSiteLocation.GetSelectListOperationalSiteLocations();
        }

        public List<SelectListItem> GetSelectListOperationalSiteLocationsByAssetOwnwer(long assetOwnerID)
        {
            AssetOwner assetOwner = repositoryAssetOwner.FindById(assetOwnerID);

            var data = new List<SelectListItem>();

            if (assetOwner.OperationalSiteId != null)
            {
                data = repositoryOperationalSiteLocation.GetOperationalSiteLocationsByOwner(assetOwner.OperationalSiteId.Value)
                .Select(x => new SelectListItem
                {
                    Value = x.OperationalSiteLocationID.ToString(),
                    Text = x.Location.LocationDescription
                })
                .OrderBy(x => x.Text)
                .ToList();
            }

            return data;
        }

        public List<OperationalSiteLocation> GetOperationalSiteLocationsByOwner(long ownerID)
        {
            List<OperationalSiteLocation> operationalSiteLocations = new List<OperationalSiteLocation>();

            AssetOwner assetOwner = repositoryAssetOwner.FindById(ownerID);

            if (assetOwner.OperationalSiteId != null)
            {
                operationalSiteLocations = repositoryOperationalSiteLocation.GetOperationalSiteLocationsByOwner(assetOwner.OperationalSiteId.Value);
            }

            return operationalSiteLocations;
        }

        public List<RackLocation> GetRackLocationsIDByOperationalSiteLocation(long operationalSiteLocationID)
        {
            List<RackLocation> rackLocations = new List<RackLocation>();

            if (operationalSiteLocationID != null)
            {
                OperationalSiteLocation operationalSiteLocation = repositoryOperationalSiteLocation.FindById(operationalSiteLocationID);

                rackLocations = repositoryRackLocation.GetRackLocationsByLocation(operationalSiteLocation.LocationID);
            }

            return rackLocations;
        }

        public List<SelectListItem> GetSelectListPurchaseItems()
        {
            return repositoryPurchaseItem.GetSelectListPurchaseItems();
        }

        public List<SelectListItem> GetSelectListRackLocations()
        {
            return repositoryRackLocation.GetSelectListRackLocations();
        }

        public List<SelectListItem> GetSelectListRackLocationsByOperationalSiteLocation(long? operationalSiteLocationID)
        {
            var data = new List<SelectListItem>();

            if (operationalSiteLocationID != null)
            {
                OperationalSiteLocation operationalSiteLocation = repositoryOperationalSiteLocation.FindById(operationalSiteLocationID.Value);

                data = repositoryRackLocation.GetSelectListRackLocationsByOperationalSiteLocation(operationalSiteLocation.LocationID);
            }

            return data;
        }

        public List<SelectListItem> GetSelectListRacks()
        {
            return repositoryRack.GetSelectListRacks();
        }

        public List<SelectListItem> GetSelectListURacks()
        {
            return repositoryURack.GetSelectListURacks();
        }

        public List<SelectListItem> GetSelectListWarningPeriods()
        {
            return repositoryWarningPeriod.GetSelectListWarningPeriods();
        }

        //public Tuple<string, PurchaseItem> GetProductTypeRef(long purchaseItemID)
        //{
        //    //Will check the TagNo of ProductType, but to get it, must first Get the full info of one PurchaseItem.
        //    PurchaseItem purchaseItem = repositoryPurchaseItem.FindById(purchaseItemID);
        //    string tagRef = purchaseItem.Product.ProductType.TagNo;

        //    return new Tuple<string, PurchaseItem>(tagRef, purchaseItem);

        //}

        public List<Asset> CreateAssetsWithPurchaseItem(long purchaseItemID, int qtyAdd)
        {
            //Inicialize new List of assets
            List<Asset> assets = new List<Asset>();

            //Receive the PurchaseItemID from view PurchaseItem/Create
            //and get data from the database
            var purchaseItem = repositoryPurchaseItem.FindById(purchaseItemID);
            long getStatusID = repositoryStatus.FindAssetFirstOnStockID();

            //Create one Asset for each PurchaseQty and add in the List Assets
            for (int i = 0; i < qtyAdd; i++)
            {
                Asset asset = new Asset();

                asset.PurchaseItemID = purchaseItemID;
                asset.StatusID = getStatusID;
                asset.DeliveryDate = purchaseItem.DeliveryDate;
                asset.WarningPeriodID = purchaseItem.Product.ProductType.WarningPeriodID.Value;
                assets.Add(asset);
            }

            return assets;
        }

        public Asset FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool AssetExists(long id)
        {
            return repository.AssetExists(id);
        }

        public Tuple<long, string, bool, string> CheckIfTagNumberExists(long purchaseItemID, int gotTagNumber)
        {
            string message;

            PurchaseItem purchaseItem = repositoryPurchaseItem.FindById(purchaseItemID);
            string tagRef = purchaseItem.Product.ProductType.TagNo;

            string tagNumber = "AUT" + gotTagNumber.ToString().PadLeft(5,'0') + tagRef;

            bool tagNumberExists = repository.TagNumberExists(tagNumber);

            if (tagNumberExists == false)
            {
                message = "This AssetTag: " + tagNumber + " can be used! (It doesn't exists yet!";
                return new Tuple<long, string, bool, string>(purchaseItemID, tagNumber, tagNumberExists, message);
            }

            message = "This AssetTag: " + tagNumber + " is already in use!";
            return new Tuple<long, string, bool, string>(purchaseItemID, tagNumber, tagNumberExists, message);
        }

        public Tuple<Asset, int> CreateNewAsset(long purchaseItemID, string assetTag)
        {
            Asset asset = new Asset();

            PurchaseItem purchaseItem = repositoryPurchaseItem.FindById(purchaseItemID);

            asset.AssetTag = assetTag;
            asset.PurchaseItemID = purchaseItemID;
            asset.PurchaseItem = purchaseItem;
            asset.StatusID = repositoryStatus.FindAssetFirstOnStockID();
            asset.DeliveryDate = purchaseItem.DeliveryDate;
            asset.WarningPeriodID = purchaseItem.Product.ProductType.WarningPeriodID.Value;

            int qtyAssetsStillNeedToAdd = QtyAssetsStillNeedToAdd(purchaseItemID);

            return new Tuple<Asset, int>(asset, qtyAssetsStillNeedToAdd);
        }


        public Tuple<Asset, bool, int, string> Add(Asset asset)
        {
            //Save first the asset
            repository.Add(asset);

            //the define de info for the QR code and generates the QR Code image
            asset.QrCode = TxtQrCodeGenerator(asset.AssetID);

            //save the asset again with the QR code.
            asset = repository.Update(asset);

            

            //Check in more Assets need to be added:

            bool stillNeedToAddAsset = false;
            string message = "";

            int qtyAssetsStillNeedToAdd = QtyAssetsStillNeedToAdd(asset.PurchaseItemID);

            if (qtyAssetsStillNeedToAdd == 0)
            {
                message = "All assets are generated!";
                asset = FindById(asset.AssetID);
                return new Tuple<Asset, bool, int, string>(asset, stillNeedToAddAsset, 0, message);
            }

            stillNeedToAddAsset = true;
            message = "Still need to be added " + qtyAssetsStillNeedToAdd + "x assets";

            return new Tuple<Asset, bool, int, string>(asset, stillNeedToAddAsset, qtyAssetsStillNeedToAdd, message);
        }

        private int QtyAssetsStillNeedToAdd(long purchaseItemID)
        {
            PurchaseItem purchaseItem = repositoryPurchaseItem.FindById(purchaseItemID);

            int qtyOfAssetsAlreadyAdded = repository.GetAssetQtyPerPurchaseItem(purchaseItemID);

            int qtyAssetsStillNeedToAdd = purchaseItem.PurchaseQty - qtyOfAssetsAlreadyAdded;

            return qtyAssetsStillNeedToAdd;
        }

        public List<Asset> AddList(List<Asset> assets)
        {
            //Will check the TagNo of ProductType, but to get it, must first Get the full info of one PurchaseItem.
            PurchaseItem purchaseItem = repositoryPurchaseItem.FindById(assets[0].PurchaseItemID);
            string tagRef = purchaseItem.Product.ProductType.TagNo;

            //Get the latest used AssetTag from the same TagRef (ProductType)
            string assetMaxTagNo = repository.FindMaxTagNo(tagRef);


            var index = 1;
            foreach (var item in assets)
            {
                if (assetMaxTagNo == null)
                {
                    //Create a new AssetTag for that particular TagRef
                    item.AssetTag = "AUT" + index.ToString().PadLeft(5, '0') + tagRef;
                }
                else
                {
                    //Increase the number of last used AssetTag number.
                    long newTagNo = index + long.Parse(assetMaxTagNo.Substring(3, 5));

                    //Generate the new AssetTag
                    item.AssetTag = "AUT" + newTagNo.ToString().PadLeft(5, '0') + tagRef;
                }

                ////Add QR Code only with the AssetTag numer and pok, roduct name???
                ////string txtChar = " - ";
                //string txtQrCode = "AssetTag: " + item.AssetTag;
                //item.QrCode = QrCodeGenerator(txtQrCode);

                ////Remark: if we need to have the full QR-code, than need create it after save all records, generate them and save them again.

                index++;
            }
            //Don't need to return to assets list as program does it automatically!

            //Will save the List Asset to the database and return it back with AssetID's.
            assets = repository.AddList(assets);


            //To create the QR-code
            foreach (var item in assets)
            {
                //Go first to define de text and then generates & return the QR Code image
                item.QrCode = TxtQrCodeGenerator(item.AssetID);

            }

            assets = repository.UpdateList(assets);

            return assets;
        }


        public void Update(Asset asset)
        {
            repository.Update(asset);

            //Go first to define de text and then generates & return the QR Code image
            asset.QrCode = TxtQrCodeGenerator(asset.AssetID);

            repository.Update(asset);


            AssetHistory assetHistory = repositoryAssetHistory.GetLatestAssetHistoryOfAsset(asset.AssetID);

            if (assetHistory == null || asset.StatusID != assetHistory.StatusID)
            {
                assetHistory = new AssetHistory();

                //Not working
                assetHistory.AssetID = asset.AssetID;
                assetHistory.StatusID = asset.StatusID;
                assetHistory.Datum = DateTime.Now;
                assetHistory.NameUser = userManager.Users.FirstOrDefault().UserName.Replace("@portofantwerp.com","");

                repositoryAssetHistory.Add(assetHistory);
            }


            
        }

        public List<Asset> UpdateList(List<Asset> assets)
        {
            //Info need first to be saved
            repository.UpdateList(assets);

            //before de QR-Code can be generated correctly
            foreach (var asset in assets)
            {
                asset.QrCode = TxtQrCodeGenerator(asset.AssetID);
            }

            //Save again, incl. the correct QR-Code and return the up to dated list back
            return repository.UpdateList(assets);
        }


        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public AssetOwner GetAssetOwner(long assetOwnerID)
        {
            return repositoryAssetOwner.FindById(assetOwnerID);
        }

        public AssetViewModel GetSpecificAssetAndAllSubForms(long id)
        {
            AssetViewModel assetViewModel = new AssetViewModel();

            //Get the specific Asset info...
            assetViewModel.asset = FindById(id);

            //...and get all the sub items
            assetViewModel.assetDetails = repositoryAssetDetail.GetAllAssetDetailsPerAsset(id);
            assetViewModel.backups = repositoryBackup.GetAllBackupsPerAsset(id);
            assetViewModel.assetLicenses = repositoryAssetLicense.GetAllAssetLicensesPerAsset(id);
            assetViewModel.assetHistories = repositoryAssetHistory.GetAllAssetHistoriesOfAsset(id);

            return assetViewModel;
        }

        private Byte[] TxtQrCodeGenerator(long assetID)
        {
            string txtQrCode = "";
            string txtChar = " - ";

            Asset asset = repository.FindById(assetID);

            // Tried 2x different ways of if... (to get only the info which is already filled in)
            // a) to avoid this error: "Object reference not set to an instance of an object"
            // b) check if each attribute has a value.

            txtQrCode = "AssetTag: " + asset.AssetTag;
            if (asset.PurchaseItem.Product != null) { txtQrCode += txtChar + "Product: " + asset.PurchaseItem.Product.Name; }
            txtQrCode += txtChar + "https://localhost:5001/Asset/Edit/" + assetID;

            //if (!string.IsNullOrEmpty(asset.SerieNo)) { txtQrCode += txtChar + "SerieNo: " + asset.SerieNo; }
            //if (!string.IsNullOrEmpty(asset.ServiceTag)) { txtQrCode += txtChar + "ServiceTag: " + asset.ServiceTag; }
            //if (!string.IsNullOrEmpty(asset.AssetOwner.OwnerDescription)) { txtQrCode += txtChar + "Owner: " + asset.AssetOwner.OwnerDescription; }
            //if (asset.OperationalSiteLocation != null)
            //{
            //    if (asset.OperationalSiteLocation.Location != null)
            //    {
            //        txtQrCode += !string.IsNullOrEmpty(asset.OperationalSiteLocation.Location.Building.Ref) ? txtChar + "Building: " + asset.OperationalSiteLocation.Location.Building.Ref : "";
            //        txtQrCode += !string.IsNullOrEmpty(asset.OperationalSiteLocation.Location.Building.Name) ? txtChar + " " + asset.OperationalSiteLocation.Location.Building.Name : "";

            //        if (asset.OperationalSiteLocation.Location.Floor != null)
            //        {
            //            txtQrCode += !string.IsNullOrEmpty(asset.OperationalSiteLocation.Location.Floor.Ref) ? txtChar + "Floor: " + asset.OperationalSiteLocation.Location.Floor.Ref : "";
            //        }
            //        txtQrCode += !string.IsNullOrEmpty(asset.OperationalSiteLocation.Location.RoomNo) ? txtChar + "Room: " + asset.OperationalSiteLocation.Location.RoomNo : "";
            //    }
            //}
            //if (asset.RackLocation != null)
            //{
            //    txtQrCode += !string.IsNullOrEmpty(asset.RackLocation.Rack.Name) ? txtChar + "Rack: " + asset.RackLocation.Rack.Name : "";
            //}
            //if (asset.URack != null)
            //{
            //    txtQrCode += !string.IsNullOrEmpty(asset.URack.Name) ? txtChar + "U-Rack: " + asset.URack.Name : "";
            //}

            return QrCodeGenerator(txtQrCode);
        }

        //QrCodeGenerator receive a string of information and return a Byte[] which can be 
        //send / save to the database and also send to the view!
        private Byte[] QrCodeGenerator(string txtQrCode)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return BitmapToBytesCode(qrCodeImage);
        }

        //Converts a Bitmap image.png to Bytes[]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }


        public List<Asset> GetAllAssetsNeedBackUp()
        {
            List<Asset> assets = repository.GetAllAssetsNeedBackUp();

            foreach (var item in assets)
            {
                // Get all info
                DateTime? startDate = item.UsingDateStart;
                DateTime? lastBackupDate = repositoryBackup.AssetLastBackup(item.AssetID);
                int? backupInterval = item.PurchaseItem.Product.ProductType.BackupInterval;

                // Check first if there is a backup interval, if not set it to 1 month.
                if (backupInterval == null)
                {
                    backupInterval = 1;
                }

                // Check then if there is a previous backup and check the latest date.
                // if there is one, add the interval backup time to it.
                if (lastBackupDate != null)
                {
                    item.MaxBackupDate = lastBackupDate.Value.AddMonths(backupInterval.Value);
                }
                else if (startDate != null)
                {
                    item.MaxBackupDate = startDate.Value.AddMonths(backupInterval.Value);
                }
                else
                {
                    item.MaxBackupDate = DateTime.Now.AddMonths(-2);
                }

                if (item.MaxBackupDate < DateTime.Now)
                {
                    item.BackupUrgency = "Red";
                }
                else if (item.MaxBackupDate >= DateTime.Now.AddDays(7))
                {
                    item.BackupUrgency = "Green";
                }
                else
                {
                    item.BackupUrgency = "Orange";
                }
            }

            return assets;
        }



        //public List<Asset> GetAssetsNeedBackUp()
        //{

        //    var list = GetAllAssetsNeedBackUp();

        //    var qtyRed = list
                
        //        .Count(l => l.BackupUrgency == "Red");


        //}



        public IEnumerable<Asset> GetAllAssetsEnum(string searchString, bool outSwitch)
        {
            IEnumerable<Asset> assets = repository.GetAllAssetsEnum();

            if (!string.IsNullOrEmpty(searchString))
            {
                // This doesn't work propperlly!!!
                //assets = assets.Where(s => s.AssetTag != null ? s.AssetTag.Contains(searchString) : false
                //                        || s.AssetOwner.OwnerDescription.Contains(searchString)
                //                        || s.PurchaseItem.Product.Name.Contains(searchString));


                //Installed nuget package: NinjaNye.SearchExtensions (http://ninjanye.github.io/SearchExtensions/stringsearch.html) which works really good!
                
                assets = assets.Search(s => s.AssetTag,
                                    s => s.Status != null ? s.Status.Name : "",
                                    s => s.PurchaseItem != null ? s.PurchaseItem.Product.Name : "",
                                    s => s.AssetOwner != null ? s.AssetOwner.OwnerDescription : "",
                                    s => s.OperationalSiteLocation != null ? s.OperationalSiteLocation.Location.LocationDescription : "")
                                    .Containing(searchString);
            }

            if (outSwitch == true)
            {
                assets = assets
                    .Where(a => a.Status.OutOfUse == true)
                    .ToList();
            }

            foreach (var asset in assets)
            {
                DateTime? deliveryDate = asset.DeliveryDate;
                int expirePeriodMonth = asset.ExpirePeriodMonth;
                int warningPeriod = asset.WarningPeriod.WarningPeriodMonth;

                asset.ExpireDate = deliveryDate.Value.AddMonths(expirePeriodMonth);
                asset.WarningDate = asset.ExpireDate.AddMonths(-warningPeriod);

                //First check the urgency accorting the WarningDate...
                if (asset.WarningDate < DateTime.Now)
                {
                    asset.WarningUrgency = "Red";
                }
                else if (asset.WarningDate >= DateTime.Now.AddDays(7))
                {
                    asset.WarningUrgency = "Green";
                }
                else
                {
                    asset.WarningUrgency = "Orange";
                }

                //...Then checks the urgency of the Product has NoSupport anymore from the supplier
                // and so overrules the previous check. And...
                if (asset.PurchaseItem.Product.Status.NoSupport == true)
                {
                    asset.WarningUrgency = "NoSupport";
                }

                // ... then checks if the Asset is OutOfUse. Overrules again all previous ones.
                if (asset.Status.OutOfUse == true)
                {
                    asset.WarningUrgency = "OutOfUse";
                }
            }
            return assets;
        }

        public IEnumerable<object> GetOperationalSiteGrid()
        {
            return repository.GetOperationalSiteGrid();
        }


        public Tuple<long, Asset, List<Backup>, List<AssetDetail>, List<License>> GetAssetWithSubs(long assetID)
        {
            Asset asset = FindById(assetID);
            List<Backup> backups = repositoryBackup.GetAllBackupsPerAsset(assetID);
            List<AssetDetail> assetDetails = repositoryAssetDetail.GetAllAssetDetailsPerAsset(assetID);
            List<License> licenses = repositoryAssetLicense.GetAllLicensesPerAsset(assetID);

            return new Tuple<long, Asset, List<Backup>, List<AssetDetail>, List<License>>(assetID, asset, backups, assetDetails, licenses);
        }

        public AssetCounterViewModel GetAssetCounterViewModel()
        {
            return new AssetCounterViewModel()
            {
                ExpiredItems = repository.GetExpiredItems(),
                WarrentyItems = repository.GetWarrentyItems(),
                GetBackupNeeded = repository.GetBackupNeeded(),
                GetAssetWarningPeriod = GetAllAssetsEnum("",false)
                                        .Where(x => x.WarningUrgency == "Red" || x.WarningUrgency == "Orange")
                                        .Count(),
                GetAssetWarningPeriodOrange = GetAllAssetsEnum("",false)
                                        .Where(x => x.WarningUrgency == "Orange")
                                        .Count(),
                GetAssetWarningPeriodRed = GetAllAssetsEnum("",false)
                                        .Where(x => x.WarningUrgency == "Red")
                                        .Count(),
                GetBackupNeededThisWeek = GetAllAssetsNeedBackUp()
                                            .Where(x => x.BackupUrgency == "Orange")
                                            .Count(),
                GetBackupNeededPeviousWeeks = GetAllAssetsNeedBackUp()
                                            .Where(x => x.BackupUrgency == "Red")
                                            .Count(),
                

            };
        }

        public List<SelectListItem> GetSelectListOperationalSiteLocationsByAssetOwner(long assetOwnerID)
        {
            AssetOwner assetOwner = repositoryAssetOwner.FindById(assetOwnerID);
            if (assetOwner.OperationalSiteId != null)
            {
                OperationalSite operationalSite = repositoryOperationalSite.FindById(assetOwner.OperationalSiteId.Value);
                return repositoryOperationalSiteLocation.GetSelectListOperationalSiteLocationsByAssetOwner(operationalSite.OperationalSiteID);
            }
            return null;
        }
    }
}
