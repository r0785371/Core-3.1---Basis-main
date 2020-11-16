using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{ 

    public interface ILicenseTypeService
    {
        List<LicenseType> GetAllLicenseTypes();

        Tuple<long, LicenseType, List<License>> GetAllLicenseTypesWithLicenses(long licenseTypeID);

        LicenseType FindById(long id);

        bool LicenseTypeExists(long id);

        void Update(LicenseType licenseType);

        void Add(LicenseType licenseType);

        void Remove(long id);

        void Save();
    }
}
