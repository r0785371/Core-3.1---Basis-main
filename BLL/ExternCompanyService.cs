using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ExternCompanyService : IExternCompanyService
    {
        readonly IExternCompanyRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IAssetOwnerRepository repositoryAssetOwner;

        public ExternCompanyService(IExternCompanyRepository _repository, IAssetRepository _repositoryAsset, 
                                    IAssetOwnerRepository _repositoryAssetOwner)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryAssetOwner = _repositoryAssetOwner;
        }

        public List<ExternCompany> GetAllExternCompanies()
        {
            return repository.GetAllExternCompanies();
        }

        public List<SelectListItem> GetSelectListExternCompanies()
        {
            return repository.GetSelectListExternCompanies();
        }

        public Tuple<long, ExternCompany, List<Asset>> GetExternCompanyWithSub(long externCompanyID)
        {
            ExternCompany externCompany = FindById(externCompanyID);

            AssetOwner assetOwner = repositoryAssetOwner.GetAssetOwnerOfExterCompany(externCompanyID);

            List<Asset> assets = repositoryAsset.GetAssetsOfAssetOwner(assetOwner.AssetOwnerID);

            return new Tuple<long, ExternCompany, List<Asset>>(externCompanyID, externCompany, assets);
        }

        public ExternCompany FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool ExternCompanyExists(long id)
        {
            return repository.ExternCompanyExists(id);
        }

        public void Update(ExternCompany externCompany)
        {
            repository.Update(externCompany);
        }

        public void Add(ExternCompany externCompany)
        {
            repository.Add(externCompany);

            repositoryAssetOwner.AddAssetOwnerExternCompany(externCompany.ExternCompanyID);
        }

        public void Remove(long id)
        {
            repositoryAssetOwner.RemoveAssetOwnerExternCompany(id);
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
