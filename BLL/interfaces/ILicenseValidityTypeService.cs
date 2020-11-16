using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface ILicenseValidityTypeService
    {
        List<LicenseValidityType> GetAllLicenseValidityTypes();

        Tuple<long, LicenseValidityType, List<License>> GetAllLicenseValidityTypesWithLicenses(long licenseValidityTypeID);

        LicenseValidityType FindById(long id);

        bool LicenseValidityTypeExists(long id);

        void Update(LicenseValidityType licenseValidityType);

        void Add(LicenseValidityType licenseValidityType);

        void Remove(long id);

        void Save();
    }
}
