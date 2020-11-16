using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BLL.ViewModels;
using NinjaNye.SearchExtensions;

namespace BLL
{
    public class LicenseService: ILicenseService
    {
        readonly ILicenseRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IAssetLicenseRepository repositoryAssetLicense;
        readonly ILicenseValidityTypeRepository repositoryLicenseValidityType;
        readonly ILicenseTypeRepository repositoryLicenseType;
        readonly IPurchaseItemRepository repositoryPurchaseItem;
        readonly ISoftwareTypeRepository repositorySoftwareType;
        readonly IStatusRepository repositoryStatus;
        private readonly IHostingEnvironment hostingEnv;

        public LicenseService(ILicenseRepository _repository, IAssetRepository _repositoryAsset,
            IAssetLicenseRepository _repositoryAssetLicense, ILicenseValidityTypeRepository _repositoryLicenseValidityType,
            ILicenseTypeRepository _repositoryLicenseType, IPurchaseItemRepository _repositoryPurchaseItem,
            ISoftwareTypeRepository _repositorySoftwareType, IStatusRepository _repositoryStatus, IHostingEnvironment _hostingEnv)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryAssetLicense = _repositoryAssetLicense;
            repositoryLicenseValidityType = _repositoryLicenseValidityType;
            repositoryLicenseType = _repositoryLicenseType;
            repositoryPurchaseItem = _repositoryPurchaseItem;
            repositorySoftwareType = _repositorySoftwareType;
            repositoryStatus = _repositoryStatus;
            hostingEnv = _hostingEnv;
        }

        public List<License> GetAllLicenses()
        {
            return repository.GetAllLicenses();
        }

        public IEnumerable<License> GetAllLicensesEnum(string searchString)
        {
            IEnumerable<License> licenses = repository.GetAllLicensesEnum();

            if (!string.IsNullOrEmpty(searchString))
            {
                //Installed nuget package: NinjaNye.SearchExtensions (http://ninjanye.github.io/SearchExtensions/stringsearch.html) which works really good!

                licenses = licenses.Search(h => h.PurchaseItem.Product.Name ?? "",
                    h => h.Status != null ? h.Status.Name : "",
                    h => h.No ?? "",
                    h => h.Key ?? "",
                    h => h.LicenseType.Name ?? "",
                    h => h.LicenseValidityType.Name ?? "")
                    .Containing(searchString);
            }

            return licenses;
        }

        public List<License> GetAllOrSelectedLicenses(long? selectedSoftwareTypeID)
        {
            List<License> licenses = new List<License>();

            if (selectedSoftwareTypeID == null)
            {
                licenses = repository.GetAllLicenses();
            }
            else
            {
                licenses = repository.GetAllLicencesOfLicenseType(selectedSoftwareTypeID.Value);
            }
                     

            foreach (var item in licenses)
            {
                if (item.LicenseType.LimitedUse == true)
                {
                    int qtyAssetLicensesPerLicense = repositoryAssetLicense.QtyAssetLicensePerLicense(item.LicenseID);

                    //if (item.QtyLimited > qtyAssetLicensesPerLicense)
                    //{
                        // If this license has still qty not used! Than changed the QtyLimited to the real one.
                        item.QtyLimited = item.QtyLimited - qtyAssetLicensesPerLicense;
                    //}
                }

                if (item.LicenseType.UnlimitedUse == true)
                {
                    item.QtyLimited = 88;
                }
            }

            return licenses;
        }

        public List<License> GetAllUnusedLicenses()
        {
            return repository.GetAllUnusedLicenses();

        }

        public List<SelectListItem> GetSelectListAssets()
        {
            return repositoryAsset.GetSelectListAssets();
        }

        public List<SelectListItem> GetSelectListLicenseValidityTypes()
        {
            return repositoryLicenseValidityType.GetSelectListLicenseValidityTypes();
        }

        public List<SelectListItem> GetSelectListLicenseTypes()
        {
            return repositoryLicenseType.GetSelectListLicenseTypes();
        }

        public List<SelectListItem> GetSelectListPurchaseItems()
        {
            return repositoryPurchaseItem.GetSelectListPurchaseItems();
        }

        public List<SelectListItem> GetSelectListSoftwareTypes()
        {
            return repositorySoftwareType.GetSelectListSoftwareTypes();
        }

        public List<SelectListItem> GetSelectListStatusLicense()
        {
            return repositoryStatus.GetSelectListStatusLicense();
        }

        public List<SelectListItem> GetSelectListLicenseOfLicenseType(long licenseTypeID)
        {
            return repository.GetSelectListLicenseOfLicenseType(licenseTypeID);
        }

        public List<License> CreateLicensesWithPurchaseItem(long purchaseItemID, int qtyAdd)
        {
            //Inicialize new List of licenses
            List<License> licenses = new List<License>();
            
            //Receive the PurchaseItemID from view PurchaseItem/Create
            //and get data from the database
            var purchaseItem = repositoryPurchaseItem.FindById(purchaseItemID);
            long getStatusID = repositoryStatus.FindLicenseFirstOnStockID();

            //Create one License for each PurchaseQty and add in the List Licenses
            for (int i = 0; i < qtyAdd; i++)
            {
                License license = new License();
                license.PurchaseItemID = purchaseItemID;
                license.StatusID = getStatusID;
                licenses.Add(license);
                
            }

            return licenses;
        }

        public License FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool LicenseExists(long id)
        {
            return repository.LicenseExists(id);
        }

        public void Update(License license)
        {
            repository.Update(license);
        }

        public void Add(License license)
        {
            repository.Add(license);
        }

        public License AddList(List<License> licenses)
        {
            repository.AddList(licenses);

            License license = FindById(licenses[0].LicenseID);

            return license;
        }

        public void AddListAssetLicenses(List<AssetLicense> assetLicenses)
        {
            repositoryAssetLicense.AddListAssetLicenses(assetLicenses);
        }

        public Tuple<long, LicenseViewModel> GetLicenseWithAssets(long licenseID)
        {
            License license = FindById(licenseID);
            List<Asset> assets = repositoryAssetLicense.AssetsPerLincense(licenseID);

            LicenseViewModel licenseViewModel = new LicenseViewModel();
            licenseViewModel.license = license;
            licenseViewModel.FileDescription = license.ColFileName;
            licenseViewModel.assets = assets;

            return new Tuple<long, LicenseViewModel>(licenseID, licenseViewModel);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public Tuple<string, string, bool> UploadLicenseColPdf(long licenseID, string fileDescription, IFormFile file)
        {

            Tuple<string, string, bool> oldFile = new Tuple<string, string, bool>("", "", false);
            string newFilePath = "";
            bool hasFile = false;

            //check then if there exist already one old file
            if (licenseID != 0)
            {
                oldFile = repository.GetLicenseColPath(licenseID);
            }

            if (file != null)
            {
                //Get the original file name
                var newFileName = Path.GetFileName(file.FileName);

                //Get the extension of the file
                string newFileExt = Path.GetExtension(file.FileName);

                //Get the new file path which is under wwwroot\licenseColPdf
                // Maybe a option to change the file name...
                newFilePath = Path.Combine(hostingEnv.WebRootPath, "licenseColPdf", newFileName);

                //Check if the file is not empty
                if (newFileName.Length > 0 && newFileExt.ToLower() == ".pdf")
                {

                    // If new file is different than local one, remove the old local PDF file.
                    if (newFilePath != oldFile.Item2)
                    {
                        // string fullPath = Request.MapPath("~/uploaded/" + file);
                        if (System.IO.File.Exists(oldFile.Item2))
                        {
                            System.IO.File.Delete(oldFile.Item2);
                        }

                    }

                    //Save the new file
                    using (var fileSteam = new FileStream(newFilePath, FileMode.Create))
                    {
                        file.CopyTo(fileSteam);
                    }
                    hasFile = true;
                }
                else
                {
                    return new Tuple<string, string, bool>(oldFile.Item1, oldFile.Item2, oldFile.Item3);
                }
            }
            else
            {
                return new Tuple<string, string, bool>(oldFile.Item1, oldFile.Item2, oldFile.Item3);
            }


            return new Tuple<string, string, bool>(fileDescription, newFilePath, hasFile);
        }



        //***********************

       

        public Tuple<string, FileStream, bool> GetLicenseColPdf(long licenseID)
        {
            Tuple<string, FileStream, bool> emptyFile = new Tuple<string, FileStream, bool>("", null, false);
            FileStream stream = null;

            try
            {
                // will get the full file path from the database
                Tuple<string, string, bool> filePath = repository.GetLicenseColPath(licenseID);
                if (!string.IsNullOrEmpty(filePath.Item2))
                {
                    // will get the pdf from wwwroot\licenseColPdf\ and send it to controler via FileStream
                    stream = new FileStream(filePath.Item2, FileMode.Open, FileAccess.Read);

                    if (stream != null)
                    {
                        return new Tuple<string, FileStream, bool>(filePath.Item1, stream, filePath.Item3);
                    }

                }
            }
            catch (Exception)
            {

                //throw;
                //return emptyFile;

            }

            return emptyFile;
        }




        //?????????????????????????????????????????
        //public Tuple<string, string, bool> RemoveProductPDF(long productID)
        //{
        //    Tuple<string, string, bool> oldFile = new Tuple<string, string, bool>("", "", false);

        //    //check if there exist already one old file
        //    if (productID != 0)
        //    {
        //        oldFile = repository.GetLicenseColPath(productID);

        //        // string fullPath = Request.MapPath("~/uploaded/" + file);
        //        if (System.IO.File.Exists(oldFile.Item2))
        //        {
        //            System.IO.File.Delete(oldFile.Item2);
        //        }
        //    }

        //    return oldFile;
        //}


    }
}
