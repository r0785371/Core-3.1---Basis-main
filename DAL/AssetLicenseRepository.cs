using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class AssetLicenseRepository: IAssetLicenseRepository
    {
        readonly DataContext context;

        public AssetLicenseRepository(DataContext _context)
        {
            context = _context;
        }

        public List<AssetLicense> GetAllAssetLicenses()
        {
            return context.AssetLicenses
                .Include(p => p.License)
                    .ThenInclude(p => p.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .Include(p => p.Asset)
                    .ThenInclude(p => p.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .ToList();
        }

        public List<SelectListItem> GetSelectListAssetLicenses()
        {
            return context.AssetLicenses.Select(s => new SelectListItem
            {
                Value = s.AssetLicenseID.ToString(),
                Text = s.License.PurchaseItem.Product.Name + " " + s.Asset.PurchaseItem.Product.Name,
                //Selected=c.AssetLicenseID.Equals(1)
            }).ToList();
        }

        //Denk dat deze niet nodig is...!
        public AssetLicense FindById(long id)
        {
            return context.AssetLicenses
                .Include(p => p.License)
                    .ThenInclude(p => p.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .Include(p => p.Asset)
                    .ThenInclude(p => p.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .Where(s => s.AssetLicenseID == id)
                .Single();
        }

        public List<AssetLicense> GetAllAssetLicensesPerAsset(long id)
        {
            return context.AssetLicenses
                .Where(s => s.AssetID == id)
                .Include(p => p.License)
                    .ThenInclude(p => p.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .Include(p => p.Asset)
                    .ThenInclude(p => p.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .ToList();
        }

        public List<License> GetAllLicensesPerAsset(long assetID)
        {
            return context.Licenses
                .Where(l => l.AssetLicenses.Any(a => a.AssetID == assetID))
                .ToList();
        }

        public bool AssetLicenseExists(long id)
        {
            return context.AssetLicenses.Any(s => s.AssetLicenseID == id);
        }

        public int QtyAssetLicensePerAsset(long id)
        {
            return context.AssetLicenses
                .Where(s => s.AssetID == id)
                .ToList()
                .Count();
        }

        public int QtyAssetLicensePerLicense(long licenseID)
        {
            return context.AssetLicenses
                .Where(s => s.LicenseID == licenseID)
                .ToList()
                .Count();
        }

        public List<Asset> AssetsPerLincense(long licenseID)
        {

            // This is special! Will count how many assets are with this special license.
            // Special thing is that there is a many-to-many relation (table inbetween which is AssetLicense)
            return context.Assets
                .Where(a => a.AssetLicenses.Any(l => l.LicenseID == licenseID))
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.People)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.GroupPeople)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.OperationalSite)
                .Include(a => a.AssetOwner)
                    .ThenInclude(a => a.Warehouse)
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

        public void Add(AssetLicense assetLicense)
        {
            context.AssetLicenses.Add(assetLicense);
            context.SaveChanges();
        }

        public void AddListAssetLicenses(List<AssetLicense> assetLicenses)
        {
            context.AssetLicenses.AddRange(assetLicenses);
            context.SaveChanges();
        }

        public void Update(AssetLicense assetLicense)
        {
            context.AssetLicenses.Update(assetLicense);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var assetLicense = context.AssetLicenses.SingleOrDefault(s => s.AssetLicenseID == id);
            context.AssetLicenses.Remove(assetLicense);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
