using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class URackRepository: IURackRepository
    {
        readonly DataContext context;

        public URackRepository(DataContext _context)
        {
            context = _context;
        }

        public List<URack> GetAllURacks()
        {
            return context.URacks
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListURacks()
        {
            return context.URacks.Select(s => new SelectListItem
            {
                Value = s.URackID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public URack FindById(long id)
        {
            return context.URacks.Where(s => s.URackID == id).Single();
        }

        public bool URackExists(long id)
        {
            return context.URacks.Any(s => s.URackID == id);
        }

        public void Add(URack rack)
        {
            context.URacks.Add(rack);
            context.SaveChanges();
        }

        public void Update(URack rack)
        {
            context.URacks.Update(rack);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var rack = context.URacks.SingleOrDefault(s => s.URackID == id);
            context.URacks.Remove(rack);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
