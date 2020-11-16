using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class FloorRepository: IFloorRepository
    {
        readonly DataContext context;

        public FloorRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Floor> GetAllFloors()
        {
            return context.Floors
                .OrderBy(o => o.Sequence)
                .ToList();
        }

        public List<SelectListItem> GetSelectListFloors()
        {
            return context.Floors
                .OrderBy(o => o.Sequence)
                .Select(s => new SelectListItem
            {
                Value = s.FloorID.ToString(),
                Text = s.Name,
            }).ToList();
        }

        public Floor FindById(long id)
        {
            return context.Floors.Where(s => s.FloorID == id).Single();
        }

        public bool FloorExists(long id)
        {
            return context.Floors.Any(s => s.FloorID == id);
        }

        public void Add(Floor floor)
        {
            context.Floors.Add(floor);
            context.SaveChanges();
        }

        public void Update(Floor floor)
        {
            context.Floors.Update(floor);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var floor = context.Floors.SingleOrDefault(s => s.FloorID == id);
            context.Floors.Remove(floor);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
