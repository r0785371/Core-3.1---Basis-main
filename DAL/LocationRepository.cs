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
    public class LocationRepository: ILocationRepository
    {
        readonly DataContext context;

        public LocationRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Location> GetAllLocations()
        {
            return context.Locations
                .Include(l => l.Building)
                .Include(l => l.Floor)
                .Include(l =>l.Room)
                .Include(l =>l.Warehouses)
                .OrderBy(o => o.Building.Ref)
                .ToList();
        }

        public List<SelectListItem> GetSelectListLocations()
        {
            return GetAllLocations()
                .OrderBy(o => o.Building.Ref)
                .Select(s => new SelectListItem
            {
                Value = s.LocationID.ToString(),
                Text = s.LocationDescription,
            }).ToList();
        }

        public List<SelectListItem> GetListLocationsIsWarehouse()
        {
            return GetAllLocations()
                .Where(l => l.IsWarehouse == true)
                .OrderBy(o => o.Building.Ref)
                .Select(s => new SelectListItem
                {
                    Value = s.LocationID.ToString(),
                    Text = s.LocationDescription,
                }).ToList();
        }

        public List<Location> GetAllLocationsOfBuilding(long buildingID)
        {
            return context.Locations
                .Where(l => l.BuildingId == buildingID)
                .Include(l => l.Building)
                .Include(l => l.Floor)
                .Include(l => l.Room)
                .Include(l => l.Warehouses)
                .OrderBy(o => o.Building.Ref)
                .ToList();
        }

        public List<Location> GetAllLocationsOfFloor(long floorID)
        {
            return context.Locations
                .Where(l => l.FloorId == floorID)
                .Include(l => l.Building)
                .Include(l => l.Floor)
                .Include(l => l.Room)
                .Include(l => l.Warehouses)
                .OrderBy(o => o.Building.Ref)
                .ToList();
        }

        public List<Location> GetAllLocationsOfRoom(long roomID)
        {
            return context.Locations
                .Where(l => l.RoomId == roomID)
                .Include(l => l.Building)
                .Include(l => l.Floor)
                .Include(l => l.Room)
                .Include(l => l.Warehouses)
                .OrderBy(o => o.Building.Ref)
                .ToList();
        }

        public List<SelectListItem> GetSelectListAssetOwners()
        {
            return context.AssetOwners.Select(s => new SelectListItem
            {
                Value = s.AssetOwnerID.ToString(),
                Text = s.OperationalSite.Name + s.People.FullName + s.Warehouse.Name + s.GroupPeople.GroupName,
            }).OrderBy(o => o.Text).ToList();
        }

        public Location FindById(long id)
        {
            return context.Locations
                .Where(s => s.LocationID == id)
                .Include(l => l.Building)
                .Include(l => l.Floor)
                .Include(l => l.Room)
                .Include(l => l.Warehouses)
                .Single();
        }

        public bool LocationExists(long id)
        {
            return context.Locations.Any(s => s.LocationID == id);
        }

        public void Add(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }

        public void Update(Location location)
        {
            context.Locations.Update(location);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var location = context.Locations.SingleOrDefault(s => s.LocationID == id);
            context.Locations.Remove(location);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
