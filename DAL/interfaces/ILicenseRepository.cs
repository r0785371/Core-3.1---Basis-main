using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ILicenseRepository
    {
        List<License> GetAllLicenses();

        IEnumerable<License> GetAllLicensesEnum();

        List<License> GetAllLicencesOfLicenseType(long selectedSoftwareTypeID);

        List<License> GetAllUnusedLicenses();

        List<SelectListItem> GetSelectListLicenses();

        List<License> GetAllLicensesOfStatus(long statusID);

        List<License> GetAllLicensesOfLicenseType(long licenseTypeID);

        List<License> GetLicenseOfPurchaseItem(long purchaseItemID);

        List<License> GetAllLicensesOfLicenseValidityType(long licenseValidityTypeID);

        List<SelectListItem> GetSelectListLicenseOfLicenseType(long licenseTypeID);

        License FindById(long id);

        bool LicenseExists(long id);

        void Update(License license);

        void Add(License license);

        void AddList(List<License> licenses);

        void UpdateList(List<License> licenses);

        void Remove(long id);

        void Save();

        int GetLicenseQtyPerPurchaseItem(long purchaseItemID);

        //List<AssetLicense> GetAllLicensesPerAsset(long id);

        Tuple<string, string, bool> GetLicenseColPath(long licenseID);

    }
}
