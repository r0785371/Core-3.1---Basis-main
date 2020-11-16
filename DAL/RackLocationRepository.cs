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
    public class RackLocationRepository: IRackLocationRepository
    {
        readonly DataContext context;

        public RackLocationRepository(DataContext _context)
        {
            context = _context;
        }

        public List<RackLocation> GetAllRackLocations()
        {
            return context.RackLocations
                .Include(r => r.Assets)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Building)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Floor)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Room)
                .Include(r => r.Rack)
                .ToList();
        }

        public List<SelectListItem> GetSelectListRackLocations()
        {
            return context.RackLocations.Select(s => new SelectListItem
            {
                Value = s.RackLocationID.ToString(),
                Text = s.RackNo,
                //Selected=c.RackLocationID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public List<SelectListItem> GetSelectListRackLocationsByOperationalSiteLocation(long locationID)
        {
            return context.RackLocations
                .Where(r => r.LocationID == locationID)
                .Select(x => new SelectListItem
                {
                    Value = x.RackLocationID.ToString(),
                    Text = x.RackNo + " - " + x.Rack.Name,
                })
                .OrderBy(o => o.Text)
                .ToList();
        }

        public List<RackLocation> GetAllRackLocationsOfRack(long rackID)
        {
            return context.RackLocations
                .Where(r => r.RackID == rackID)
                .Include(r => r.Assets)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Building)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Floor)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Room)
                .Include(r => r.Rack)
                .ToList();
        }

        public List<RackLocation> GetRackLocationsByLocation(long locationID)
        {
            return context.RackLocations
                .Where(r => r.LocationID == locationID)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Building)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Floor)
                .Include(r => r.Location)
                    .ThenInclude(r => r.Room)
                .Include(r => r.Rack)
                .ToList();
        }

        public RackLocation FindById(long id)
        {
            return context.RackLocations
                .Where(s => s.RackLocationID == id)
                .Include(r => r.Assets)
                .Include(r => r.Location)
                .Include(r => r.Rack)
                .Single();
        }

        public bool RackLocationExists(long id)
        {
            return context.RackLocations.Any(s => s.RackLocationID == id);
        }

        public void Add(RackLocation rackLocation)
        {
            context.RackLocations.Add(rackLocation);
            context.SaveChanges();
        }

        public void Update(RackLocation rackLocation)
        {
            context.RackLocations.Update(rackLocation);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var rackLocation = context.RackLocations.SingleOrDefault(s => s.RackLocationID == id);
            context.RackLocations.Remove(rackLocation);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
