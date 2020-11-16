using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL.interfaces
{
    public interface ILicenseService
    {
        List<License> GetAllLicenses();

        IEnumerable<License> GetAllLicensesEnum(string searchString);

        List<License> GetAllOrSelectedLicenses(long? selectedSoftwareTypeID);

        List<License> GetAllUnusedLicenses();

        List<SelectListItem> GetSelectListAssets();

        List<SelectListItem> GetSelectListLicenseValidityTypes();

        List<SelectListItem> GetSelectListLicenseTypes();

        List<SelectListItem> GetSelectListPurchaseItems();

        List<SelectListItem> GetSelectListSoftwareTypes();

        List<SelectListItem> GetSelectListStatusLicense();

        List<SelectListItem> GetSelectListLicenseOfLicenseType(long licenseTypeID);

        List<License> CreateLicensesWithPurchaseItem(long purchaseItemID, int qtyAdd);

        License FindById(long id);

        bool LicenseExists(long id);

        void Update(License license);

        void Add(License license);

        License AddList(List<License> licenses);

        void AddListAssetLicenses(List<AssetLicense> assetLicenses);

        Tuple<long, LicenseViewModel> GetLicenseWithAssets(long licenseID);

        void Remove(long id);

        void Save();

        Tuple<string, string, bool> UploadLicenseColPdf(long licenseID, string fileDescription, IFormFile file);

        Tuple<string, FileStream, bool> GetLicenseColPdf(long licenseID);
    }
}
