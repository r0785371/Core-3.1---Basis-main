using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class LicenseValidityTypeRepository: ILicenseValidityTypeRepository
    {
        readonly DataContext context;

        public LicenseValidityTypeRepository(DataContext _context)
        {
            context = _context;
        }

        public List<LicenseValidityType> GetAllLicenseValidityTypes()
        {
            return context.LicenseValidityTypes
                //.Include(a => a.WarningPeriod)
                .ToList();
        }

        public List<SelectListItem> GetSelectListLicenseValidityTypes()
        {
            return context.LicenseValidityTypes.Select(s => new SelectListItem
            {
                Value = s.LicenseValidityTypeID.ToString(),
                Text = s.Name,
                //Selected=c.LicenseValidityTypeID.Equals(1)
            }).ToList();
        }

        public LicenseValidityType FindById(long id)
        {
            return context.LicenseValidityTypes.Where(s => s.LicenseValidityTypeID == id).Single();
        }

        public bool LicenseValidityTypeExists(long id)
        {
            return context.LicenseValidityTypes.Any(s => s.LicenseValidityTypeID == id);
        }

        public void Add(LicenseValidityType licenseValidityType)
        {
            context.LicenseValidityTypes.Add(licenseValidityType);
            context.SaveChanges();
        }

        public void Update(LicenseValidityType licenseValidityType)
        {
            context.LicenseValidityTypes.Update(licenseValidityType);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var licenseValidityType = context.LicenseValidityTypes.SingleOrDefault(s => s.LicenseValidityTypeID == id);
            context.LicenseValidityTypes.Remove(licenseValidityType);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
