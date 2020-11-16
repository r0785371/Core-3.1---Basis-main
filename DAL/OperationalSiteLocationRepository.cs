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
    public class OperationalSiteLocationRepository: IOperationalSiteLocationRepository
    {
        readonly DataContext context;

        public OperationalSiteLocationRepository(DataContext _context)
        {
            context = _context;
        }

        public List<OperationalSiteLocation> GetAllOperationalSiteLocations()
        {
            return context.OperationalSiteLocations
                .Include(o => o.Location)
                    .ThenInclude(o => o.Building)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Floor)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Room)
                .Include(o => o.OperationalSite)
                .ToList();
        }

        public List<OperationalSiteLocation> GetAllOperationalSiteLocationsPerOS(long id, long? operationalSiteGroupId)
        {
            


            // Show all OperationalSitesLocations from that OperationalSite, but also from the ones which
            // belong to his "group"
            return context.OperationalSiteLocations
                .Where(s => s.OperationalSiteID == id || s.OperationalSite.OperationalSiteID == operationalSiteGroupId)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Building)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Floor)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Room)
                .Include(o => o.OperationalSite)
                .ToList();
        }

        public List<SelectListItem> GetSelectListOperationalSiteLocations()
        {
            return context.OperationalSiteLocations.Select(s => new SelectListItem
            {
                Value = s.OperationalSiteLocationID.ToString(),
                //Text = s.Location.Building.Ref + " " + s.Location.Building.Name + " " + s.Location.Floor.Ref + "/"
                //       + s.Location.Room.Ref + " - " + s.Location.RoomNo,
                //Text = s.Location.Building.Ref != null && s.Location.Room.Name != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref + " / "
                //       + s.Location.Room.Name + " - " + s.Location.RoomNo :
                //       s.Location.Room.Name != null ? s.Location.Building.Name + " / " + s.Location.Floor.Ref + " / "
                //       + s.Location.Room.Name + " - " + s.Location.RoomNo :
                //       s.Location.Floor.Ref != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref :
                //       s.Location.Building.Ref + " " + s.Location.Building.Name,


                Text =  s.Location.Building.Ref != null && s.Location.Building.Name != null && s.Location.Floor.Ref != null && s.Location.Room.Name != null && s.Location.RoomNo != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref + " / " + s.Location.Room.Name + " - " + s.Location.RoomNo :
                        s.Location.Building.Ref != null && s.Location.Building.Name != null && s.Location.Floor.Ref != null && s.Location.Room.Name != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref + " / " + s.Location.Room.Name :
                        s.Location.Building.Ref != null && s.Location.Building.Name != null && s.Location.Floor.Ref != null && s.Location.RoomNo != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref + " / " + s.Location.RoomNo :
                        s.Location.Building.Ref != null && s.Location.Building.Name != null && s.Location.Floor.Ref != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref :
                        s.Location.Building.Ref != null && s.Location.Building.Name != null && s.Location.Room.Name != null && s.Location.RoomNo != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Room.Name + " - " + s.Location.RoomNo :
                        s.Location.Building.Ref != null && s.Location.Building.Name != null ? s.Location.Building.Ref + " " + s.Location.Building.Name :

                        s.Location.Building.Ref != null ? s.Location.Building.Ref : "",
            }).OrderBy(o => o.Text).ToList();
        }

        public List<OperationalSiteLocation> GetOperationalSiteLocationsByOwner(long operationalSiteId)
        {
            return context.OperationalSiteLocations

                            .Where(o => o.OperationalSiteID == operationalSiteId || o.OperationalSite.OperationalSiteGroupId == operationalSiteId)
                            .Include(o => o.OperationalSite)
                            .Include(o => o.Location)
                            .ThenInclude(o => o.Building)
                            .Include(o => o.Location)
                            .ThenInclude(o => o.Floor)
                            .Include(o => o.Location)
                            .ThenInclude(o => o.Room)
                            .ToList();
            
        }

        public List<SelectListItem> GetSelectListOperationalSiteLocationsByAssetOwner(long operationalSiteID)
        {
            return context.OperationalSiteLocations
                .Where(o => o.OperationalSiteID == operationalSiteID)
                .Include(o => o.Location)
                .Select(s => new SelectListItem
                {
                    Value = s.OperationalSiteLocationID.ToString(),
                    //Text = s.Location.LocationDescription
                    Text = s.Location.Room.Name != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref + " / "
                       + s.Location.Room.Name + " - " + s.Location.RoomNo :
                       s.Location.Floor.Ref != null ? s.Location.Building.Ref + " " + s.Location.Building.Name + " / " + s.Location.Floor.Ref :
                       s.Location.Building.Ref + " " + s.Location.Building.Name,
                })
                .ToList();



            //var states = (from s in dbContext.States
            //              where s.CountryId == countryId
            //              select new
            //              {
            //                  id = s.Id,
            //                  state = s.Name
            //              }).ToArray();

        }

        public OperationalSiteLocation FindById(long id)
        {
            return context.OperationalSiteLocations
                .Where(s => s.OperationalSiteLocationID == id)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Building)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Floor)
                .Include(o => o.Location)
                    .ThenInclude(o => o.Room)
                .Include(o => o.OperationalSite)
                .Single();
        }

        public bool OperationalSiteLocationExists(long id)
        {
            return context.OperationalSiteLocations.Any(s => s.OperationalSiteLocationID == id);
        }

        public void Add(OperationalSiteLocation operationalSiteLocation)
        {
            context.OperationalSiteLocations.Add(operationalSiteLocation);
            context.SaveChanges();
        }

        public void Update(OperationalSiteLocation operationalSiteLocation)
        {
            context.OperationalSiteLocations.Update(operationalSiteLocation);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var operationalSiteLocation = context.OperationalSiteLocations.SingleOrDefault(s => s.OperationalSiteLocationID == id);
            context.OperationalSiteLocations.Remove(operationalSiteLocation);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
