using BLL.interfaces;
using BLL.ViewModels;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class PurchaseService: IPurchaseService
    {
        readonly IPurchaseRepository repository;
        readonly ISupplierRepository repositorySupplier;
        readonly IPurchaseItemRepository repositoryPurchaseItem;
        readonly IPurchaseTypeRepository repositoryPurchaseType;

        public PurchaseService(IPurchaseRepository _repository, ISupplierRepository _repositorySupplier, IPurchaseItemRepository _repositoryPurchaseItem, IPurchaseTypeRepository _repositoryPurchaseType)
        {
            repository = _repository;
            repositorySupplier = _repositorySupplier;
            repositoryPurchaseItem = _repositoryPurchaseItem;
            repositoryPurchaseType = _repositoryPurchaseType;
        }

        public List<Purchase> GetAllPurchases()
        {
            return repository.GetAllPurchases();
        }

        public List<SelectListItem> GetSelectListSuppliers()
        {
            return repositorySupplier.GetSelectListSuppliers();
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            return repositoryPurchaseType.GetAllPurchaseTypes();
        }

        //public List<SelectListItem> GetSelectListPurchaseTypes()
        //{
        //    return repositoryPurchaseType.GetSelectListPurchaseTypes();
        //}

        public int QtyPurchaseItemsPerPurchase(long id)
        {
            return repositoryPurchaseItem.QtyPurchaseItemsPerPurchase(id);
        }

        public Purchase FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool PurchaseExists(long id)
        {
            return repository.PurchaseExists(id);
        }

        public void Update(Purchase purchase)
        {
            repository.Update(purchase);
        }

        public Purchase Add(Purchase purchase)
        {
            return repository.Add(purchase);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public purchaseViewModel LoadPurchaseSubFormPurchaseItems(long id)
        {
            purchaseViewModel purchaseViewModel = new purchaseViewModel();

            //Get the specific Purchase info...
            purchaseViewModel.purchase = FindById(id);
            purchaseViewModel.purchase.ListPurchaseTypes = repositoryPurchaseType.GetAllPurchaseTypes();
            purchaseViewModel.SelectedPurchaseTypeID = purchaseViewModel.purchase.PurchaseTypeID;

            //and get then all PurchaseItems.
            purchaseViewModel.purchaseItems = repositoryPurchaseItem.GetAllPurchaseItemsPerPurchase(id);


            return purchaseViewModel;
        }

    }
}
