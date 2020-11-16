using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ILicenseTypeRepository
    {
        List<LicenseType> GetAllLicenseTypes();

        List<SelectListItem> GetSelectListLicenseTypes();

        LicenseType FindById(long id);

        bool LicenseTypeExists(long id);

        void Update(LicenseType licenseType);

        void Add(LicenseType licenseType);

        void Remove(long id);

        void Save();
    }
}
