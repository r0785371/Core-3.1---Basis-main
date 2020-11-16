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
    public class DetailService: IDetailService
    {
        readonly IDetailRepository repository;
        readonly IDetailMainRepository repositoryDetailMain;
        readonly IDetailSubRepository repositoryDetailSub;
        readonly IAssetDetailRepository repositoryAssetDetail;
        readonly IProductDetailRepository repositoryProductDetail;
        readonly IProductTypeRepository repositoryProductType;

        public DetailService(IDetailRepository _repository, IDetailMainRepository _repositoryDetailMain,
                                IDetailSubRepository _repositoryDetailSub, IAssetDetailRepository _repositoryAssetDetail, 
                                IProductDetailRepository _repositoryProductDetail, IProductTypeRepository _repositoryProductType)
        {
            repository = _repository;
            repositoryDetailMain = _repositoryDetailMain;
            repositoryDetailSub = _repositoryDetailSub;
            repositoryAssetDetail = _repositoryAssetDetail;
            repositoryProductDetail = _repositoryProductDetail;
            repositoryProductType = _repositoryProductType;
        }

        public List<Detail> GetAllDetails()
        {
            return repository.GetAllDetails();
        }

        public List<SelectListItem> GetSelectListDetailMains()
        {
            return repositoryDetailMain.GetSelectListDetailMains();
        }

        public List<SelectListItem> GetSelectListDetailSubs()
        {
            return repositoryDetailSub.GetSelectListDetailSubs();
        }

        public List<SelectListItem> GetSelectListProductTypes()
        {
            return repositoryProductType.GetSelectListProductTypes();
        }

        public Tuple<long, Detail, List<ProductDetail>, List<AssetDetail>> GetAllDetailsWithSub(long detailID)
        {
            Detail detail = FindById(detailID);

            List<ProductDetail> productDetails = repositoryProductDetail.GetAllProductDetailsOfDetail(detailID);

            List<AssetDetail> assetDetails = repositoryAssetDetail.GetAllAssetDetailsOfDetail(detailID);

            return new Tuple<long, Detail, List<ProductDetail>, List<AssetDetail>>(detailID, detail, productDetails, assetDetails);
        }

        public Detail FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool DetailExists(long id)
        {
            return repository.DetailExists(id);
        }

        public void Update(Detail assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(Detail assetDetail)
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
