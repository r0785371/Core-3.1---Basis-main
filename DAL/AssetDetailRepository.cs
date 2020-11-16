using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AssetDetailRepository: IAssetDetailRepository
    {
        readonly DataContext context;

        public AssetDetailRepository(DataContext _context)
        {
            context = _context;
        }

        public List<AssetDetail> GetAllAssetDetails()
        {
            return context.AssetDetails
                .Include(a => a.Asset)
                .Include(a => a.Detail)
                .ToList();
        }

        public List<SelectListItem> GetSelectListAssetDetails()
        {
            return context.AssetDetails.Select(s => new SelectListItem
            {
                Value = s.AssetDetailID.ToString(),
                Text = s.Detail.DetailMain.Name + " " + s.Detail.DetailSub.Name,
                //Selected=c.AssetDetailID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public List<AssetDetail> GetAllAssetDetailsOfDetail(long detailID) 
        {
            return context.AssetDetails
                .Where(a => a.DetailID == detailID)
                .Include(a => a.Asset)
                    .ThenInclude(a => a.PurchaseItem)
                        .ThenInclude(a => a.Product)
                .Include(a => a.Asset)
                    .ThenInclude(a => a.Status)
                .Include(a => a.Asset)
                    .ThenInclude(a => a.AssetOwner)
                        .ThenInclude(a => a.People)
                .Include(a => a.Asset)
                    .ThenInclude(a => a.AssetOwner)
                        .ThenInclude(a => a.GroupPeople)
                .Include(a => a.Asset)
                    .ThenInclude(a => a.AssetOwner)
                        .ThenInclude(a => a.OperationalSite)
                .Include(a => a.Asset)
                    .ThenInclude(a => a.AssetOwner)
                        .ThenInclude(a => a.Warehouse)
                .Include(a => a.Detail)
                .ToList();
        }

        public AssetDetail FindById(long id)
        {
            return context.AssetDetails
                .Where(s => s.AssetDetailID == id)
                .Include(a => a.Asset)
                .Include(a => a.Detail)
                .Single();
        }

        public bool AssetDetailExists(long id)
        {
            return context.AssetDetails.Any(s => s.AssetDetailID == id);
        }

        public void Add(AssetDetail assetDetail)
        {
            context.AssetDetails.Add(assetDetail);
            context.SaveChanges();
        }

        public void Update(AssetDetail assetDetail)
        {
            context.AssetDetails.Update(assetDetail);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var assetDetail = context.AssetDetails.SingleOrDefault(s => s.AssetDetailID == id);
            context.AssetDetails.Remove(assetDetail);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<AssetDetail> GetAllAssetDetailsPerAsset(long id)
        {
            return context.AssetDetails
                .Where(s => s.AssetID == id)
                .Include(p => p.Detail)
                .ThenInclude(p => p.DetailMain)
                .Include(p => p.Detail)
                .ThenInclude(p => p.DetailSub)
                .ToList();
        }
    }
}
