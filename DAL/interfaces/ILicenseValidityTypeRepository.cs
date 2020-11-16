using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ILicenseValidityTypeRepository
    {
        List<LicenseValidityType> GetAllLicenseValidityTypes();

        List<SelectListItem> GetSelectListLicenseValidityTypes();

        LicenseValidityType FindById(long id);

        bool LicenseValidityTypeExists(long id);

        void Update(LicenseValidityType licenseValidityType);

        void Add(LicenseValidityType licenseValidityType);

        void Remove(long id);

        void Save();
    }
}
