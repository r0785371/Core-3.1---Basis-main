using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using BLL.interfaces;
using DAL.interfaces;

namespace BLL
{
    public class LicenseTypeService: ILicenseTypeService
    {
        readonly ILicenseTypeRepository repository;
        readonly ILicenseRepository repositoryLicense;

        public LicenseTypeService(ILicenseTypeRepository _repository, ILicenseRepository _repositoryLicense)
        {
            repository = _repository;
            repositoryLicense = _repositoryLicense;
        }

        public List<LicenseType> GetAllLicenseTypes()
        {
            return repository.GetAllLicenseTypes();
        }

        public Tuple<long, LicenseType, List<License>> GetAllLicenseTypesWithLicenses(long licenseTypeID)
        {
            LicenseType licenseType = FindById(licenseTypeID);

            List<License> licenses = repositoryLicense.GetAllLicensesOfLicenseType(licenseTypeID);

            return new Tuple<long, LicenseType, List<License>>(licenseTypeID, licenseType, licenses);
        }

        public LicenseType FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool LicenseTypeExists(long id)
        {
            return repository.LicenseTypeExists(id);
        }

        public void Update(LicenseType assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(LicenseType assetDetail)
        {
            repository.Add(assetDetail);
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
