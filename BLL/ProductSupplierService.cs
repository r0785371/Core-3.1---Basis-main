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
    public class ProductSupplierService: IProductSupplierService
    {
        readonly IProductSupplierRepository repository;
        readonly ISupplierRepository repositorySupplier;

        public ProductSupplierService(IProductSupplierRepository _repository, ISupplierRepository _repositorySupplier)
        {
            repository = _repository;
            repositorySupplier = _repositorySupplier;
        }

        public List<SelectListItem> GetSelectListAllSuppliers()
        {
            return repositorySupplier.GetSelectListSuppliers();
        }

        public List<SelectListItem> GetSuppliersOfProductChild(string productChildren)
        {
            return repositorySupplier.GetSelectListSpecificSuppliers(productChildren);
        }

        public ProductSupplier FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool ProductSupplierExists(long id)
        {
            return repository.ProductSupplierExists(id);
        }

        public void Update(ProductSupplier productSupplier)
        {
            repository.Update(productSupplier);
        }

        public void Add(ProductSupplier productSupplier)
        {
            repository.Add(productSupplier);
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
