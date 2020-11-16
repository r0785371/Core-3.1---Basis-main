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
    public class AssetDetailService: IAssetDetailService
    {
        readonly IAssetDetailRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IDetailRepository repositoryDetail;

        public AssetDetailService(IAssetDetailRepository _repository, IAssetRepository _repositoryAsset, 
            IDetailRepository _repositoryDetail)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryDetail = _repositoryDetail;
        }

        public List<AssetDetail> GetAllAssetDetails()
        {
            return repository.GetAllAssetDetails();
        }

        public List<SelectListItem> GetSelectListDetails()
        {
            return repositoryDetail.GetSelectListDetails();
        }

        public List<SelectListItem> GetSelectListDetailsOfProductType(long assetID)
        {
            Asset asset = repositoryAsset.FindById(assetID);

            return repositoryDetail.GetSelectListDetailsOfProductType(asset.PurchaseItem.Product.ProductTypeID);
        }

        public AssetDetail FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool AssetDetailExists(long id)
        {
            return repository.AssetDetailExists(id);
        }

        public void Update(AssetDetail assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(AssetDetail assetDetail)
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
