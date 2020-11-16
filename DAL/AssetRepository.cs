using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Dynamic;
//using NinjaNye.SearchExtensions;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DAL
{
    public class AssetRepository : IAssetRepository
    {
        readonly DataContext context;

        public AssetRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Asset> GetAllAssets()
        {
            return context.Assets
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                        .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                        .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                        .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.Status)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.ProductSuppliers)
                            .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }


        public IEnumerable<Asset> GetAllAssetsEnum()
            {
            return context.Assets
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                        .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                        .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                        .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.Status)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.ProductSuppliers)
                            .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();

        }



        ////Installed nuget package: NinjaNye.SearchExtensions (http://ninjanye.github.io/SearchExtensions/stringsearch.html) which works really good!
        //assets = assets.Search(s => s.AssetTag,
        //                               s => s.Status != null ? s.Status.Name : "",
        //                               s => s.PurchaseItem != null ? s.PurchaseItem.Product.Name : "",
        //                               s => s.AssetOwner != null ? s.AssetOwner.OwnerDescription : "",
        //                               s => s.OperationalSiteLocation != null ? s.OperationalSiteLocation.Location.LocationDescription : "")
        //                               .Containing(searchString);



        public List<Asset> GetAllAssetsOfPuchaseItem(long purchaseItemID)
        {
            return context.Assets
                .Where(a => a.PurchaseItemID == purchaseItemID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }

        public List<Asset> GetAllAssetsOfStatus(long statusID)
        {
            return context.Assets
                .Where(a => a.StatusID == statusID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }

        public List<Asset> GetAllAssetsOfOperationalSiteLocation(long operationalSiteLocationID)
        {
            return context.Assets
                .Where(a => a.OperationalSiteLocationID == operationalSiteLocationID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }

        public List<Asset> GetAssetsOfAssetOwner(long id)
        {
            if (id > 0)
            {
                return context.Assets
                .Where(a => a.AssetOwnerID == id)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
            }
            return null;
        }

        public List<Asset> GetAllAssetsOfWarningPeriod(long warningPeriodID)
        {
            return context.Assets
                .Where(a => a.WarningPeriodID == warningPeriodID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }

        public List<SelectListItem> GetSelectListAssets()
        {
            return context.Assets.Select(s => new SelectListItem
            {
                Value = s.AssetID.ToString(),
                Text = s.AssetTag,
                //Selected=c.AssetID.Equals(1)
            }).ToList();
        }

        public List<Asset> GetAllAssetsOfRackLocation(long rackLocationID)
        {
            return context.Assets
                .Where(a => a.RackLocationID == rackLocationID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }

        public List<Asset> GetAllAssetsOfUrack(long uRackID)
        {
            return context.Assets
                .Where(a => a.URackID == uRackID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();
        }

        public List<Asset> GetAllAssetsOfAssetOwner(long assetOwnerID)
        {
            return context.Assets
                .Where(a => a.AssetOwnerID == assetOwnerID)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductSuppliers)
                    .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .ToList();

        }

        public Asset FindById(long id)
        {
            return context.Assets
                .Where(s => s.AssetID == id)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
                .Include(a => a.Status)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Building)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
                .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.ProductSuppliers)
                            .ThenInclude(a => a.Supplier)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                        .ThenInclude(a => a.ProductType)
                .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Purchase)
                        .ThenInclude(a => a.Supplier)
                .Include(a => a.RackLocation)
                    .ThenInclude(a => a.Rack)
                .Include(a => a.URack)
                .Include(a => a.WarningPeriod)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseType)
                .Include(a => a.AssetLicenses)
                    .ThenInclude(a => a.License)
                        .ThenInclude(a => a.LicenseValidityType)
                .Single();
        }

        public string FindMaxTagNo(string tagRef)
        {
            return context.Assets
                .Include(a => a.PurchaseItem)
                .ThenInclude(a => a.Product)
                .ThenInclude(a => a.ProductType)
                .Where(a => a.PurchaseItem.Product.ProductType.TagNo == tagRef)
                .Max(a => a.AssetTag);
        }

        //public string FindMaxTagNo(long productTypeID)
        //{
        //    return context.Assets
        //        .Where(a => a.PurchaseItem.Product.ProductTypeID == productTypeID)
        //        .Max(a => a.AssetTag);
        //}

        public bool AssetExists(long id)
        {
            return context.Assets.Any(s => s.AssetID == id);
        }

        public bool TagNumberExists(string tagNumber)
        {
            return context.Assets.Any(s => s.AssetTag == tagNumber);
        }

        public void Add(Asset asset)
        {
            context.Assets.Add(asset);
            context.SaveChanges();
        }

        public List<Asset> AddList(List<Asset> assets)
        {
            context.Assets.AddRange(assets);
            context.SaveChanges();
            return assets;
        }

        public Asset Update(Asset asset)
        {

            ////Checks if StatusID is changed from original
            //if (context.Entry(asset).Property(a => a.StatusID).IsModified)
            //{

            //}

            context.Assets.Update(asset);
            context.SaveChanges();
            return asset;
        }

        public List<Asset> UpdateList(List<Asset> assets)
        {
            context.Assets.UpdateRange(assets);
            context.SaveChanges();
            return assets;
        }

        public void Remove(long id)
        {
            var asset = context.Assets.SingleOrDefault(s => s.AssetID == id);
            context.Assets.Remove(asset);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int QtyAssetsPerAssetOwner(long id)
        {
            return context.Assets
                .Where(s => s.AssetOwnerID == id)
                .ToList()
                .Count();
        }

        public List<Asset> GetAllAssetsNeedBackUp()
        {
            return context.Assets
               .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
               .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
               .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
               .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.ExternCompany)
               .Include(a => a.Status)
               .Include(a => a.OperationalSiteLocation)
                   .ThenInclude(a => a.Location)
                   .ThenInclude(a => a.Building)
               .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Floor)
               .Include(a => a.OperationalSiteLocation)
                    .ThenInclude(a => a.Location)
                    .ThenInclude(a => a.Room)
               .Include(a => a.PurchaseItem)
                    .ThenInclude(a => a.Product)
                    .ThenInclude(a => a.ProductType)
               .Include(a => a.RackLocation)
               .Include(a => a.URack)
               .Include(a => a.WarningPeriod)
               .Where(a => a.Status.InUse == true && a.PurchaseItem.Product.ProductType.HasBackup == true)
               .ToList();

        }


        public int GetAssetQtyPerPurchaseItem(long purchaseItemID)
        {
            var assetQty = context.Assets
                .Where(a => a.PurchaseItemID == purchaseItemID)
                .ToList()
                .Count();

            return assetQty;
        }
        public int GetWarrentyItems()
        {
            return context.Assets.Count(x => x.DeliveryDate.AddMonths(x.ExpirePeriodMonth) > DateTime.Now);
        }

        public int GetExpiredItems()
        {
            return context.Assets.Count(x => x.DeliveryDate.AddMonths(x.ExpirePeriodMonth) <= DateTime.Now);
        }

        public int GetExpiredBackups()
        {

            var list = context.Assets
                .Where(a => a.Status.InUse == true && a.PurchaseItem.Product.ProductType.HasBackup == true)
                
                .ToList();




            return context.Assets.Count(x => x.DeliveryDate.AddMonths(x.ExpirePeriodMonth) <= DateTime.Now);
        }

        public int GetBackupNeeded()
        {
            return context.Assets
                .Count(x => x.Status.InUse == true && x.PurchaseItem.Product.ProductType.HasBackup == true);
        }




        //***************************************************************************************************

        
        //public IEnumerable<object> GetOperationalSiteGrid()
        //{
        //    List<Asset> assets = GetAllAssets();

        //https://www.codeproject.com/Questions/1091909/Dynamic-pivoting-in-linq-Csharp-MVC

        //    //dynamic headers
        //    var qry1 = assets.GroupBy(a => new { a.AssetOwner })
        //    .Select(g => new
        //    {
        //        OwnerDescription = g.Key.AssetOwner.OwnerDescription,

        //        subject = g.GroupBy(f => f.PurchaseItem.Product.ProductType.Ref).Select(m => new { Sub = m.Key, Score = m.Count() })
        //    });

        //    return qry1.ToList();
        //}

        public IEnumerable<object> GetOperationalSiteGrid()
        {

            //https://stackoverflow.com/questions/167304/is-it-possible-to-pivot-data-using-linq

            var query = GetAllAssets()
                            .GroupBy(c => c.AssetOwner.OwnerDescription)
                            .Select(assets =>
                            {
                                var results = new OperationalSiteStatistics();
                                foreach (var asset in assets)
                                {
                                    switch (asset.PurchaseItem.Product.ProductTypeID)
                                    {
                                        case 1:
                                            results.Computer++;
                                            break;
                                        case 2:
                                            results.Switch++;
                                            break;
                                        case 3:
                                            results.Drive++;
                                            break;
                                        case 4:
                                            results.Ups++;
                                            break;
                                        case 5:
                                            results.PLC++;
                                            break;
                                        case 7:
                                            results.EngStation++;
                                            break;
                                        case 8:
                                            results.ScadaServer++;
                                            break;
                                        case 10:
                                            results.Laptop++;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                return new
                                {
                                    AssetOwner = assets.Key,
                                    results.Computer,
                                    results.Switch,
                                    results.Drive,
                                    results.Ups,
                                    results.PLC,
                                    results.EngStation,
                                    results.ScadaServer,
                                    results.Laptop,
                                };
                            });

            return query;
        }

        //bar chart 15-07
        public int BackupNeededAsset()
        {
            return context.Assets.Count(c => c.Status.InUse == true && c.PurchaseItem.Product.ProductType.HasBackup == true);
        }

        public int AllAssets()
        {
            return context.Assets.Count();
        }

        //public int AllAssets()
        //{
        //    return context.Assets.Count();
        //}











        //public List<object> GetOperationalSitePivot()
        //{
        //    List<Asset> myList = GetAllAssets();

        //    var query = myList
        //        .GroupBy(c => c.OperationalSiteLocationID)
        //        .Select(g =>
        //        {
        //            OperationalSiteStatistics results = g.Aggregate(new OperationalSiteStatistics(), (result, asset) => result.Accumulate(asset), operationalSiteStatistics => operationalSiteStatistics.Compute());
        //            return new
        //            {
        //                OperatingSystemID = g.Key,
        //                results.Computer,
        //                results.Switch,
        //                results.Drive,
        //                results.Ups,
        //                results.PLC,
        //                results.License,
        //                results.Laptop

        //};
        //        });
        //    Console.ReadKey();
        //}






    }
}
