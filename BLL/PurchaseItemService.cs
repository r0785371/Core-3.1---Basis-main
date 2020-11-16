using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;

namespace BLL
{
    public class PurchaseItemService: IPurchaseItemService
    {
        readonly IHardwareRepository repositoryHardware;
        readonly IPurchaseItemRepository repository;
        readonly IPurchaseRepository repositoryPurchase;
        readonly IProductRepository repositoryProduct;
        readonly IStatusRepository repositoryStatus;
        readonly IProductTypeRepository repositoryProductType;
        readonly IAssetRepository repositoryAsset;
        readonly ILicenseRepository repositoryLicense;

        public PurchaseItemService(IPurchaseItemRepository _repository, IPurchaseRepository _repositoryPurchase, IProductRepository _repositoryProduct,
        IStatusRepository _repositoryStatus, IProductTypeRepository _repositoryProductType, IAssetRepository _repositoryAsset, ILicenseRepository _repositoryLicense)
        {
            repository = _repository;
            repositoryPurchase = _repositoryPurchase;
            repositoryProduct = _repositoryProduct;
            repositoryStatus = _repositoryStatus;
            repositoryProductType = _repositoryProductType;
            repositoryAsset = _repositoryAsset;
            repositoryLicense = _repositoryLicense;
        }

        public List<PurchaseItem> GetAllPurchaseItems()
        {
            return repository.GetAllPurchaseItems();
        }

        public List<SelectListItem> GetSelectListProducts()
        {
            return repositoryProduct.GetSelectListProducts();
        }

        public List<SelectListItem> GetSelectListActiveProducts()
        {
            return repositoryProduct.GetSelectListActiveProducts();
        }

        public List<SelectListItem> GetSelectListProductsOfSupplier(long purchaseID, bool? hasHardware, bool? hasSoftware)
        {
            Purchase purchase = repositoryPurchase.FindById(purchaseID);

            return repositoryProduct.GetSelectListProductsOfSupplier(purchase.SupplierID, hasHardware, hasSoftware);
        }

        public List<SelectListItem> GetSelectListStatusPurchase()
        {
            return repositoryStatus.GetSelectListStatusPurchase();
        }

        public Tuple<long, PurchaseItem, List<Asset>, List<License>> GetPurchaseItemWithSub(long purchaseItemID)
        {
            PurchaseItem purchaseItem = FindById(purchaseItemID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfPuchaseItem(purchaseItemID);

            List<License> licenses = repositoryLicense.GetLicenseOfPurchaseItem(purchaseItemID);

            return new Tuple<long, PurchaseItem, List<Asset>, List<License>>(purchaseItemID, purchaseItem, assets, licenses);
        }

        public PurchaseItem FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool PurchaseItemExists(long id)
        {
            return repository.PurchaseItemExists(id);
        }

        public Tuple<string, int> Update(PurchaseItem purchaseItem)
        {
            repository.Update(purchaseItem);

            return CheckIfNeedToAddAssetOrLicense(purchaseItem.PurchaseItemID);

            //// Get back the purchaseItem with all (sub-)data
            //purchaseItem = FindById(purchaseItem.PurchaseItemID);


            //// Need to check if there are already Asset(s) / License(s) generated for this PurchaseItem

            //if (purchaseItem.Product.ProductType.ProductChild.ToString() == "Hardware")
            //{
            //    if (purchaseItem.Status.GenerateAssetOrLicense == true && purchaseItem.Status.HasAsset == true)
            //    {
            //        int assetQty = GetAssetQtyPerPurchaseItem(purchaseItem.PurchaseItemID);
            //        int difAssetQty = purchaseItem.PurchaseQty - assetQty;

            //        if (difAssetQty > 0)
            //        {
            //            return new Tuple<string, int>(purchaseItem.Product.ProductType.ProductChild.ToString(), difAssetQty);
            //        }
            //    }
            //}

            //if (purchaseItem.Product.ProductType.ProductChild.ToString() == "Software")
            //{
            //    if (purchaseItem.Status.GenerateAssetOrLicense == true && purchaseItem.Status.HasLicense == true)
            //    {
            //        int licenseQty = GetLicenseQtyPerPurchaseItem(purchaseItem.PurchaseItemID);
            //        int difLicenseQty = purchaseItem.PurchaseQty - licenseQty;

            //        if (difLicenseQty > 0)
            //        {
            //            return new Tuple<string, int>(purchaseItem.Product.ProductType.ProductChild.ToString(), difLicenseQty);
            //        }
            //    }
            //}

            //return new Tuple<string, int>("", 0);

        }

        public List<ProductType> GetAllProductTypes()
        {
            return repositoryProductType.GetAllProductTypes();
        }

        public Tuple<string, int> Add(PurchaseItem purchaseItem)
        {
            purchaseItem = repository.Add(purchaseItem);

            return CheckIfNeedToAddAssetOrLicense(purchaseItem.PurchaseItemID);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public int GetAssetQtyPerPurchaseItem(long purchaseItemID)
        {
            return repositoryAsset.GetAssetQtyPerPurchaseItem(purchaseItemID);
        }

        public int GetLicenseQtyPerPurchaseItem(long purchaseItemID)
        {
            return repositoryLicense.GetLicenseQtyPerPurchaseItem(purchaseItemID);
        }

        private Tuple<string, int> CheckIfNeedToAddAssetOrLicense(long purchaseItemID)
        {

            // Get back the purchaseItem with all (sub-)data
            PurchaseItem purchaseItem = FindById(purchaseItemID);


            // Need to check if there are already Asset(s) / License(s) generated for this PurchaseItem

            if (purchaseItem.Product.ProductType.ProductChild.ToString() == "Hardware")
            {
                if (purchaseItem.Status.GenerateAssetOrLicense == true && purchaseItem.Status.HasAsset == true)
                {
                    int assetQty = GetAssetQtyPerPurchaseItem(purchaseItem.PurchaseItemID);
                    int difAssetQty = purchaseItem.PurchaseQty - assetQty;

                    if (difAssetQty > 0)
                    {
                        return new Tuple<string, int>(purchaseItem.Product.ProductType.ProductChild.ToString(), difAssetQty);
                    }
                }
            }

            if (purchaseItem.Product.ProductType.ProductChild.ToString() == "Software")
            {
                if (purchaseItem.Status.GenerateAssetOrLicense == true && purchaseItem.Status.HasLicense == true)
                {
                    int licenseQty = GetLicenseQtyPerPurchaseItem(purchaseItem.PurchaseItemID);
                    int difLicenseQty = purchaseItem.PurchaseQty - licenseQty;

                    if (difLicenseQty > 0)
                    {
                        return new Tuple<string, int>(purchaseItem.Product.ProductType.ProductChild.ToString(), difLicenseQty);
                    }
                }
            }

            return new Tuple<string, int>("", 0);
        }

        public List<PurchaseItem> GetListPurchaseItemsToOrder()
        {
            return repository.GetListPurchaseItemsToOrder();
        }

        public List<PurchaseItem> GetListPurchaseItemsOrdered()
        {
            return repository.GetListPurchaseItemsOrdered();
        }

    }
}
