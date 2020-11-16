using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class SoftwareTypeRepository: ISoftwareTypeRepository
    {
        readonly DataContext context;

        public SoftwareTypeRepository(DataContext _context)
        {
            context = _context;
        }

        public List<SoftwareType> GetAllSoftwareTypes()
        {
            return context.SoftwareTypes
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListSoftwareTypes()
        {
            return context.SoftwareTypes.Select(s => new SelectListItem
            {
                Value = s.SoftwareTypeID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public SoftwareType FindById(long id)
        {
            return context.SoftwareTypes.Where(s => s.SoftwareTypeID == id).Single();
        }

        public bool SoftwareTypeExists(long id)
        {
            return context.SoftwareTypes.Any(s => s.SoftwareTypeID == id);
        }

        public void Add(SoftwareType softwareType)
        {
            context.SoftwareTypes.Add(softwareType);
            context.SaveChanges();
        }

        public void Update(SoftwareType softwareType)
        {
            context.SoftwareTypes.Update(softwareType);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var softwareType = context.SoftwareTypes.SingleOrDefault(s => s.SoftwareTypeID == id);
            context.SoftwareTypes.Remove(softwareType);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
