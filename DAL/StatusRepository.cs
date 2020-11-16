using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class StatusRepository: IStatusRepository
    {
        readonly DataContext context;

        public StatusRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Status> GetAllStatus()
        {
            return context.Status
                .ToList();
        }

        //public List<SelectListItem> GetSelectListStatus()
        //{
        //    return context.Status.Select(s => new SelectListItem
        //    {
        //        Value = s.StatusID.ToString(),
        //        Text = s.Name,
        //        //Selected=c.StatusID.Equals(1)
        //    }).ToList();
        //}

        public List<SelectListItem> GetSelectListStatusProduct()
        {
            return context.Status
                .Where(s => s.HasProduct==true)
                .OrderBy(s => s.ProductSequence)
                .Select(s => new SelectListItem
            {
                Value = s.StatusID.ToString(),
                Text = s.Name,
                //Selected=s.HasProduct.Equals(true)
            }).ToList();
        }

        public List<SelectListItem> GetSelectListStatusPurchase()
        {
            return context.Status
                .Where(s => s.HasPurchase == true)
                .OrderBy(s => s.PurchaseSequence)
                .Select(s => new SelectListItem
                {
                    Value = s.StatusID.ToString(),
                    Text = s.Name,
                }).ToList();
        }

        public List<SelectListItem> GetSelectListStatusAsset()
        {
            return context.Status
                .Where(s => s.HasAsset == true)
                .OrderBy(s => s.AssetSequence)
                .Select(s => new SelectListItem
                {
                    Value = s.StatusID.ToString(),
                    Text = s.Name,
                }).ToList();
        }

        public List<SelectListItem> GetSelectListStatusLicense()
        {
            return context.Status
                .Where(s => s.HasLicense == true)
                .OrderBy(s => s.LicenceSequence)
                .Select(s => new SelectListItem
                {
                    Value = s.StatusID.ToString(),
                    Text = s.Name,
                }).ToList();
        }

        public Status FindById(long id)
        {
            return context.Status.Where(s => s.StatusID == id).Single();
        }



        public bool StatusExists(long id)
        {
            return context.Status.Any(s => s.StatusID == id);
        }

        public void Add(Status status)
        {
            context.Status.Add(status);
            context.SaveChanges();
        }

        public void Update(Status status)
        {
            context.Status.Update(status);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var status = context.Status.SingleOrDefault(s => s.StatusID == id);
            context.Status.Remove(status);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public long FindLicenseFirstOnStockID()
        {
            var status = context.Status
                .Where(s => s.HasLicense == true && s.OnStock == true)
                .OrderBy(s => s.LicenceSequence)
                .FirstOrDefault();

            return status.StatusID;
        }

        public long FindAssetFirstOnStockID()
        {
            var status = context.Status
                .Where(s => s.HasAsset == true && s.OnStock == true)
                .OrderBy(s => s.AssetSequence)
                .FirstOrDefault();

            return status.StatusID;
        }
    }
}
