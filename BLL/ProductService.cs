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
    public class ProductService : IProductService
    {
        readonly IProductRepository repository;
        readonly IProductTypeRepository repositoryProductType;
        readonly IStatusRepository repositoryStatus;

        public ProductService(IProductRepository _repository, IProductTypeRepository _repositoryProductType, IStatusRepository _repositoryStatus)
        {
            repository = _repository;
            repositoryProductType = _repositoryProductType;
            repositoryStatus = _repositoryStatus;
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public List<SelectListItem> GetSelectListProductTypes()
        {
            return repositoryProductType.GetSelectListProductTypes();
        }

        public List<SelectListItem> GetSelectListStatusProduct()
        {
            return repositoryStatus.GetSelectListStatusProduct();
        }

        public Product FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool ProductExists(long id)
        {
            return repository.ProductExists(id);
        }

        public void Update(Product product)
        {
            repository.Update(product);
        }

        public void Add(Product product)
        {
            repository.Add(product);
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
