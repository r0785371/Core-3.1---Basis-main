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
    public class WarningPeriodService: IWarningPeriodService
    {
        readonly IWarningPeriodRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IProductTypeRepository repositoryProductType;

        public WarningPeriodService(IWarningPeriodRepository _repository, IAssetRepository _repositoryAsset,
                                    IProductTypeRepository _repositoryProductType)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryProductType = _repositoryProductType;
        }

        public List<WarningPeriod> GetAllWarningPeriods()
        {
            return repository.GetAllWarningPeriods();
        }

        public Tuple<long, WarningPeriod, List<Asset>, List<ProductType>> GetAllWarningPeriodsWithSubs(long warningPeriodID)
        {
            WarningPeriod warningPeriod = FindById(warningPeriodID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfWarningPeriod(warningPeriodID);

            List<ProductType> productTypes = repositoryProductType.GetAllProductTypesOfWarningPeriod(warningPeriodID);

            return new Tuple<long, WarningPeriod, List<Asset>, List<ProductType>> (warningPeriodID, warningPeriod, assets, productTypes);
        }

        public WarningPeriod FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool WarningPeriodExists(long id)
        {
            return repository.WarningPeriodExists(id);
        }

        public void Update(WarningPeriod assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(WarningPeriod assetDetail)
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
