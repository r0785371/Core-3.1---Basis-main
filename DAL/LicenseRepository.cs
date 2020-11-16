using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using NinjaNye.SearchExtensions;

namespace DAL
{
    public class LicenseRepository : ILicenseRepository
    {
        readonly DataContext context;

        public LicenseRepository(DataContext _context)
        {
            context = _context;
        }

        public List<License> GetAllLicenses()
        {
            return context.Licenses
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();
        }

        public IEnumerable<License> GetAllLicensesEnum()
        {
            return context.Licenses
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();
        }

        public List<License> GetAllLicencesOfLicenseType(long selectedSoftwareTypeID)
        {
            List<License> licenses = context.Licenses
                .Where(l => (l.PurchaseItem.Product as Software).SoftwareTypeID == selectedSoftwareTypeID)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                        .ThenInclude(l => (l as Software).SoftwareType)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();

            return licenses;
        }

        public List<License> GetAllUnusedLicenses()
        {
            // Generates a Left Anti join (is a Left Outer join without the Inner join, that's why the where a == null,
            // but, we want also to see the Unlimited Licenses as they can be used many times.
            // unfortunately these once come double and so we have to remove them later.

            List<License> leftList = (from li in context.Licenses
                                      join a in context.AssetLicenses
                                      on li.LicenseID equals a.LicenseID into output
                                      from a in output.DefaultIfEmpty()
                                      where a == null || li.LicenseType.UnlimitedUse == true
                                      select new License()
                                      {
                                          LicenseID = li.LicenseID,
                                          PurchaseItemID = li.PurchaseItemID,
                                          No = li.No,
                                          Key = li.Key,
                                          HasCol = li.HasCol,
                                          ColFileName = li.ColFileName,
                                          ValidityTypeTime = li.ValidityTypeTime,
                                          LicenseTypeID = li.LicenseTypeID,
                                          LicenseValidityTypeID = li.LicenseValidityTypeID,
                                          AddToAsset = li.AddToAsset,
                                          AssetLicenseID = li.AssetLicenseID,
                                          StatusID = li.StatusID

                                      }).ToList();

            // Then we also want to add the "Limited" Licenses which still are not all used to the leftlist.

            // First get a list of all "Limited" Licenses:
            List<License> licenses = GetAllLicenses()
                        .Where(l => l.LicenseType.LimitedUse == true)
                        .ToList();

            

            // Then check each one if the quantity of Limited License is bigger then the quantity of 
            // this license is already used.

            foreach (var item in licenses)
            {
                var limited = context.AssetLicenses
                        .Where(a => a.LicenseID == item.LicenseID)
                        .Count();

                if (item.QtyLimited > limited)
                {
                    // If this license has still qty not used! Than changed the QtyLimited to the real one.
                    item.QtyLimited = item.QtyLimited - limited;
                    leftList.Add(item);
                }
            }

            // Remove the dupplicate ones comparing LicenseID
            // here we are first grouping the result by heb LicenseID and then picking the first item from each group (License).
            List<License> leftListWithoutDopplicates = leftList.GroupBy(i => i.LicenseID).Select(g => g.First()).ToList();

            return leftListWithoutDopplicates;
        }

        public List<License> GetLicensesNotAssositedToAsset()
        {
            return context.Licenses
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .Where(l => l.AssetLicenses.Any(p => p.LicenseID != 0))
                .ToList();
        }

        public List<SelectListItem> GetSelectListLicenses()
        {
            return context.Licenses.Select(s => new SelectListItem
            {
                Value = s.LicenseID.ToString(),
                Text = s.PurchaseItem.Product.Name,
                //Selected=c.LicenseID.Equals(1)
            }).ToList();
        }

        public List<License> GetAllLicensesOfStatus(long statusID)
        {
            return context.Licenses
                .Where(l => l.StatusID == statusID)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();
        }

        public List<License> GetAllLicensesOfLicenseType(long licenseTypeID)
        {
            return context.Licenses
                .Where(l => l.LicenseTypeID == licenseTypeID)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();
        }

        public List<License> GetLicenseOfPurchaseItem(long purchaseItemID)
        {
            return context.Licenses
                .Where(l => l.PurchaseItemID == purchaseItemID)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();
        }

        public List<License> GetAllLicensesOfLicenseValidityType(long licenseValidityTypeID)
        {
            return context.Licenses
                .Where(l => l.LicenseValidityTypeID == licenseValidityTypeID)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .ToList();
        }

        public List<SelectListItem> GetSelectListLicenseOfLicenseType(long licenseTypeID)
        {
            return context.Licenses
                .Where(l => l.LicenseTypeID == licenseTypeID)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Select(s => new SelectListItem
                {
                    Value = s.LicenseID.ToString(),
                    Text = s.PurchaseItem.Product.Name + " " + s.PurchaseItem.Purchase.No + " " + s.PurchaseItem.Purchase.Date,
                }).ToList();
        }

        public License FindById(long id)
        {
            return context.Licenses
                .Where(s => s.LicenseID == id)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Purchase)
                    .ThenInclude(s => s.Supplier)
                .Include(l => l.PurchaseItem)
                    .ThenInclude(l => l.Product)
                .Include(l => l.LicenseType)
                .Include(l => l.LicenseValidityType)
                .Include(l => l.AssetLicenses)
                .Include(l => l.Status)
                .Single();
        }

        public bool LicenseExists(long id)
        {
            return context.Licenses.Any(s => s.LicenseID == id);
        }

        public void Add(License license)
        {
            context.Licenses.Add(license);
            context.SaveChanges();
        }

        public void AddList(List<License> licenses)
        {
            context.Licenses.AddRange(licenses);
            context.SaveChanges();
        }

        public void UpdateList(List<License> licenses)
        {
            context.Licenses.UpdateRange(licenses);
            context.SaveChanges();
        }

        public void Update(License license)
        {
            //As we call the methode GetSpecificationFilePathOfProduct te get the full path of the productPdf
            // https://entityframework.net/knowledge-base/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-type-with-the-same-key-is-already-being-tracked

            // 
            var local = context.Set<License>()
                .Local
                .FirstOrDefault(entry => entry.LicenseID.Equals(license.LicenseID));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }

            // set Modified flag in your entry
            context.Entry(license).State = EntityState.Modified;

            // save 
            context.SaveChanges();


            ////old
            //context.Licenses.Update(license);
            //context.SaveChanges();
        }

        public void Remove(long id)
        {
            var license = context.Licenses.SingleOrDefault(s => s.LicenseID == id);
            context.Licenses.Remove(license);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int GetLicenseQtyPerPurchaseItem(long purchaseItemID)
        {
            var licenseQty = context.Licenses
                .Where(l => l.PurchaseItemID == purchaseItemID)
                .ToList()
                .Count();

            return licenseQty;
        }

        public Tuple<string, string, bool> GetLicenseColPath(long licenseID)
        {
            var license = context.Licenses
                .Where(h => h.LicenseID == licenseID)
                .Single();

            if (string.IsNullOrEmpty(license.ColFileName))
            {
                license.ColFileName = "";
            }

            if (string.IsNullOrEmpty(license.ColFilePath))
            {
                license.ColFilePath = "";
            }

            return new Tuple<string, string, bool>(license.ColFileName, license.ColFilePath, license.HasCol);
        }
    }
}
