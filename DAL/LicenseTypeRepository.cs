using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class LicenseTypeRepository: ILicenseTypeRepository
    {
        readonly DataContext context;

        public LicenseTypeRepository(DataContext _context)
        {
            context = _context;
        }

        public List<LicenseType> GetAllLicenseTypes()
        {
            return context.LicenseTypes
                //.Include(a => a.WarningPeriod)
                .ToList();
        }

        public List<SelectListItem> GetSelectListLicenseTypes()
        {
            return context.LicenseTypes.Select(s => new SelectListItem
            {
                Value = s.LicenseTypeID.ToString(),
                Text = s.Name,
                //Selected=c.LicenseTypeID.Equals(1)
            }).ToList();
        }

        public LicenseType FindById(long id)
        {
            return context.LicenseTypes.Where(s => s.LicenseTypeID == id).Single();
        }

        public bool LicenseTypeExists(long id)
        {
            return context.LicenseTypes.Any(s => s.LicenseTypeID == id);
        }

        public void Add(LicenseType licenseType)
        {
            context.LicenseTypes.Add(licenseType);
            context.SaveChanges();
        }

        public void Update(LicenseType licenseType)
        {
            context.LicenseTypes.Update(licenseType);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var licenseType = context.LicenseTypes.SingleOrDefault(s => s.LicenseTypeID == id);
            context.LicenseTypes.Remove(licenseType);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
