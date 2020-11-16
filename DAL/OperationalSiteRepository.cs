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
   public class OperationalSiteRepository: IOperationalSiteRepository
    {
        readonly DataContext context;

        public OperationalSiteRepository(DataContext _context)
        {
            context = _context;
        }

        public List<OperationalSite> GetAllOperationalSites()
        {
            return context.OperationalSites
                .Include(o => o.OperationalSiteGroup)
                .Include(o => o.AssetOwer)
                    //.ThenInclude(o => o.)
                .Include(o => o.OperationalSiteLocations)
                    .ThenInclude(o => o.Location)
                        .ThenInclude(o => o.Building)
                .Include(o => o.OperationalSiteLocations)
                    .ThenInclude(o => o.Location)
                        .ThenInclude(o => o.Floor)
                .Include(o => o.OperationalSiteLocations)
                    .ThenInclude(o => o.Location)
                        .ThenInclude(o => o.Room)
                    //.ThenInclude(o => o.)
                .ToList();
        }

        public List<SelectListItem> GetSelectListOperationalSites()
        {
            return context.OperationalSites.Select(s => new SelectListItem
            {
                Value = s.OperationalSiteID.ToString(),
                Text = s.Name,
            }).ToList();
        }

        public List<SelectListItem> GetSelectOperationalSites(long operationalSiteID)
        {
            return context.OperationalSites
                .Where(o => o.OperationalSiteID == operationalSiteID || o.OperationalSiteGroupId == operationalSiteID)
                .Select(s => new SelectListItem
                {
                    Value = s.OperationalSiteID.ToString(),
                    Text = s.Name,
                }).ToList();
        }

        public List<SelectListItem> GetSelectListOperationalSiteGroups()
        {
            return context.OperationalSites
                .Where(o => o.IsGroup==true)
                .Select(s => new SelectListItem
                    {
                        Value = s.OperationalSiteID.ToString(),
                        Text = s.Name,
                    }).ToList();
        }


        public OperationalSite FindById(long id)
        {
            return context.OperationalSites.Where(s => s.OperationalSiteID == id).Single();
        }

        public bool OperationalSiteExists(long id)
        {
            return context.OperationalSites.Any(s => s.OperationalSiteID == id);
        }

        public long Add(OperationalSite operationalSite)
        {
            context.OperationalSites.Add(operationalSite);
            context.SaveChanges();
            return operationalSite.OperationalSiteID;
        }

        public void Update(OperationalSite operationalSite)
        {
            context.OperationalSites.Update(operationalSite);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var operationalSite = context.OperationalSites.SingleOrDefault(s => s.OperationalSiteID == id);
            context.OperationalSites.Remove(operationalSite);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
