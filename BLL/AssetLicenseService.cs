using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AssetLicenseService: IAssetLicenseService
    {
        readonly IAssetLicenseRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly ILicenseRepository repositoryLicense;

        public AssetLicenseService(IAssetLicenseRepository _repository, IAssetRepository _repositoryAsset, ILicenseRepository _repositoryLicense)
        {
            repository = _repository;
            repositoryLicense = _repositoryLicense;
            repositoryAsset = _repositoryAsset;
        }


        public List<AssetLicense> GetAllAssetLicenses()
        {
            return repository.GetAllAssetLicenses();
        }

        public List<AssetLicense> GetAllAssetLicensesPerAsset(long id)
        {
            return repository.GetAllAssetLicensesPerAsset(id);
        }

        public List<SelectListItem> GetSelectListAssets()
        {
            return repositoryAsset.GetSelectListAssets();
        }

        public List<SelectListItem> GetSelectListLicenses()
        {
            return repositoryLicense.GetSelectListLicenses();
        }

        public AssetLicense FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool AssetLicenseExists(long id)
        {
            return repository.AssetLicenseExists(id);
        }

        public void Add(AssetLicense assetLicense)
        {
            repository.Add(assetLicense);
        }

        public void Update(AssetLicense assetLicense)
        {
            repository.Update(assetLicense);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

    }
}
