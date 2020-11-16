using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class RackRepository: IRackRepository
    {
        readonly DataContext context;

        public RackRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Rack> GetAllRacks()
        {
            return context.Racks
                .ToList();
        }

        public List<SelectListItem> GetSelectListRacks()
        {
            return context.Racks.Select(s => new SelectListItem
            {
                Value = s.RackID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public Rack FindById(long id)
        {
            return context.Racks.Where(s => s.RackID == id).Single();
        }

        public bool RackExists(long id)
        {
            return context.Racks.Any(s => s.RackID == id);
        }

        public void Add(Rack rack)
        {
            context.Racks.Add(rack);
            context.SaveChanges();
        }

        public void Update(Rack rack)
        {
            context.Racks.Update(rack);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var rack = context.Racks.SingleOrDefault(s => s.RackID == id);
            context.Racks.Remove(rack);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
