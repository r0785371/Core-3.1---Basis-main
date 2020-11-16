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
    public class AssetHistoryRepository: IAssetHistoryRepository
    {
        readonly DataContext context;

        public AssetHistoryRepository(DataContext _context)
        {
            context = _context;
        }

        public List<AssetHistory> GetAllAssetHistories()
        {
            return context.AssetHistories
                .Include(d => d.Status)
                .ToList();
        }

        //public List<SelectListItem> GetSelectListAssetHistories()
        //{
        //    return context.AssetHistories.Select(s => new SelectListItem
        //    {
        //        Value = s.AssetHistoryID.ToString(),
        //        Text = s.,
        //    }).OrderBy(o => o.Text).ToList();
        //}

        public AssetHistory FindById(long id)
        {
            return context.AssetHistories
                .Where(d => d.AssetHistoryID == id)
                .Include(d => d.Status)
                .Include(d => d.Asset)
                    .ThenInclude(a => a.PurchaseItem)
                    .ThenInclude(p => p.Product)
                .Single();
        }

        public List<AssetHistory> GetAllAssetHistoriesOfAsset(long assetId)
        {
            return context.AssetHistories
                .Where(d => d.AssetID == assetId)
                .Include(d => d.Status)
                .ToList();
        }

        public AssetHistory GetLatestAssetHistoryOfAsset(long assetID)
        {
            bool exist = AssetExists(assetID);

            if (exist)
            {
                return context.AssetHistories
                                .Where(d => d.AssetID == assetID)
                                .Include(d => d.Status)
                                .OrderByDescending(d => d.Datum)
                                .FirstOrDefault();
            }
            return null;

        }

        public bool AssetHistoryExists(long id)
        {
            return context.AssetHistories.Any(d => d.AssetHistoryID == id);
        }

        public bool AssetExists(long id)
        {
            return context.AssetHistories.Any(d => d.AssetID == id);
        }

        public void Add(AssetHistory assetHistory)
        {
            context.AssetHistories.Add(assetHistory);
            context.SaveChanges();
        }

        public void Update(AssetHistory assetHistory)
        {
            context.AssetHistories.Update(assetHistory);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var detail = context.AssetHistories.SingleOrDefault(d => d.AssetHistoryID == id);
            context.AssetHistories.Remove(detail);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
