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
    public class SupplierService: ISupplierService
    {
        readonly ISupplierRepository repository;
        //readonly IProductSupplierRepository repositoryProductSupplier;
        //readonly IPurchaseRepository repositoryPurchase;
        //readonly IHardwareRepository repositoryHardware;
        //readonly ISoftwareRepository repositorySoftware;

        //public SupplierService(ISupplierRepository _repository, IProductSupplierRepository _repositoryProductSupplier,
        //    IPurchaseRepository _repositoryPurchase, IHardwareRepository _repositoryHardware, ISoftwareRepository _repositorySoftware)
        //{
        //    repository = _repository;
        //    repositoryProductSupplier = _repositoryProductSupplier;
        //    repositoryPurchase = _repositoryPurchase;
        //    repositoryHardware = _repositoryHardware;
        //    repositorySoftware = _repositorySoftware;
        //}

        public SupplierService(ISupplierRepository _repository)
        {
            repository = _repository;

        }

        public List<Supplier> GetAllSuppliers()
        {
            return repository.GetAllSuppliers();
        }

        public Supplier FindById(long id)
        {
            return repository.FindById(id);
        }

        //public Tuple<long, Supplier, List<Hardware>, List<Software>, List<Purchase>> GetSupplierWithSub(long supplierID)
        //{
        //    Supplier supplier = FindById(supplierID);

        //    List<Hardware> hardwares = repositoryHardware.GetAllHardwareOFSupplier(supplierID);

        //    List<Software> softwares = repositorySoftware.GetAllSoftwareOfSupplier(supplierID);

        //    List<Purchase> purchases = repositoryPurchase.GetAllPurchasesOfSupplier(supplierID);

        //    return new Tuple<long, Supplier, List<Hardware>, List<Software>, List<Purchase>>(supplierID, supplier, hardwares, softwares, purchases);
        //}

        public bool SupplierExists(long id)
        {
            return repository.SupplierExists(id);
        }

        public void Update(Supplier supplier)
        {
            repository.Update(supplier);
        }

        public void Add(Supplier supplier)
        {
            repository.Add(supplier);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        //public Tuple<long, Supplier, int, int> CanDeleteSupplier(long supplierID)
        //{
            
        //    Supplier supplier = FindById(supplierID);
        //    int qtyProduct = repositoryProductSupplier.QtyProductSupplierPerSupplier(supplierID);
        //    int qtyPurchase = repositoryPurchase.QtyPurchasePerSupplier(supplierID);

        //    return new Tuple<long, Supplier, int, int>(supplierID, supplier, qtyProduct, qtyPurchase);
        //}
    }
}
