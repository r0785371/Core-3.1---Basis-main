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
    public class ProductDetailService: IProductDetailService
    {
        readonly IProductDetailRepository repository;
        readonly IDetailRepository repositoryDetail;
        readonly IProductRepository repositoryProduct;

        public ProductDetailService(IProductDetailRepository _repository, IDetailRepository _repositoryDetail,
            IProductRepository _repositoryProduct)
        {
            repository = _repository;
            repositoryDetail = _repositoryDetail;
            repositoryProduct = _repositoryProduct;
        }

        public List<ProductDetail> GetAllProductDetails()
        {
            return repository.GetAllProductDetails();
        }

        public List<SelectListItem> GetSelectListDetails()
        {
            return repositoryDetail.GetSelectListDetails();
        }

        public List<SelectListItem> GetSelectListDetailsOfProductType(long productID)
        {
            Product product = repositoryProduct.FindById(productID);

            return repositoryDetail.GetSelectListDetailsOfProductType(product.ProductTypeID);
        }

        public ProductDetail FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool ProductDetailExists(long id)
        {
            return repository.ProductDetailExists(id);
        }

        public void Update(ProductDetail assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(ProductDetail assetDetail)
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
