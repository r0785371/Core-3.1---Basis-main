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
    public class LicenseValidityTypeService: ILicenseValidityTypeService
    {
        readonly ILicenseValidityTypeRepository repository;
        readonly ILicenseRepository repositoryLicense;

        public LicenseValidityTypeService(ILicenseValidityTypeRepository _repository, ILicenseRepository _repositoryLicense)
        {
            repository = _repository;
            repositoryLicense = _repositoryLicense;
        }

        public List<LicenseValidityType> GetAllLicenseValidityTypes()
        {
            return repository.GetAllLicenseValidityTypes();
        }

        public Tuple<long, LicenseValidityType, List<License>> GetAllLicenseValidityTypesWithLicenses(long licenseValidityTypeID)
        {
            LicenseValidityType licenseValidityType = FindById(licenseValidityTypeID);

            List<License> licenses = repositoryLicense.GetAllLicensesOfLicenseValidityType(licenseValidityTypeID);


            return new Tuple<long, LicenseValidityType, List<License>>(licenseValidityTypeID, licenseValidityType, licenses);
        }

        public LicenseValidityType FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool LicenseValidityTypeExists(long id)
        {
            return repository.LicenseValidityTypeExists(id);
        }

        public void Update(LicenseValidityType assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(LicenseValidityType assetDetail)
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
