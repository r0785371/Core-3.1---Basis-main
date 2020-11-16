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
    public class ProductTypeService: IProductTypeService
    {
        readonly IProductTypeRepository repository;
        readonly IWarningPeriodRepository repositoryWarningPeriod;
        readonly IProductRepository repositoryProduct;

        public ProductTypeService(IProductTypeRepository _repository, IWarningPeriodRepository _repositoryWarningPeriod,
            IProductRepository _repositoryProduct)
        {
            repository = _repository;
            repositoryWarningPeriod = _repositoryWarningPeriod;
            repositoryProduct = _repositoryProduct;
        }

        public List<ProductType> GetAllProductTypes()
        {
            return repository.GetAllProductTypes();
        }

        public List<SelectListItem> GetSelectListProductTypeGroups()
        {
            return repository.GetSelectListProductTypeGroups();
        }

        public List<SelectListItem> GetSelectListWarningPeriods()
        {
            return repositoryWarningPeriod.GetSelectListWarningPeriods();
        }

        public ProductType FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool ProductTypeExists(long id)
        {
            return repository.ProductTypeExists(id);
        }

        public void Update(ProductType productType)
        {
            repository.Update(productType);
        }

        public void Add(ProductType productType)
        {
            repository.Add(productType);
        }

        public Tuple<long, ProductType, List<Product>> GetProductTypeWithProducts(long productTypeID)
        {
            ProductType productType = FindById(productTypeID);

            List<Product> products = repositoryProduct.GetAllProductsOfProductType(productTypeID);

            return new Tuple<long, ProductType, List<Product>>(productTypeID, productType, products);
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
