using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
   public class BuildingRepository: IBuildingRepository
    {
        readonly DataContext context;

        public BuildingRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Building> GetAllBuildings()
        {
            
            return context.Buildings
                .OrderBy(o => o.Ref)
                .ToList();

        }

        public List<SelectListItem> GetSelectListBuildings()
        {
            return context.Buildings
                .OrderBy(o => o.Ref)
                .Select(s => new SelectListItem
            {
                Value = s.BuildingID.ToString(),
                Text = s.Ref + " " + s.Name,
                //Andrew :nummer hoeft er niet bij te staan
            }).ToList();
        }

        public Building FindById(long id)
        {
            return context.Buildings.Where(s => s.BuildingID == id).Single();
        }

        public bool BuildingExists(long id)
        {
            return context.Buildings.Any(s => s.BuildingID == id);
        }

        public void Add(Building building)
        {
            context.Buildings.Add(building);
            context.SaveChanges();
        }

        public void Update(Building building)
        {
            context.Buildings.Update(building);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var building = context.Buildings.SingleOrDefault(s => s.BuildingID == id);
            context.Buildings.Remove(building);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
