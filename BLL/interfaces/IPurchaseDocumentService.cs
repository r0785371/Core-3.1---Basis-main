using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
   public interface IPurchaseService
    {
        List<Purchase> GetAllPurchases();

        List<SelectListItem> GetSelectListSuppliers();

        List<PurchaseType> GetAllPurchaseTypes();
        //List<SelectListItem> GetSelectListPurchaseTypes();

        purchaseViewModel LoadPurchaseSubFormPurchaseItems(long id);

        int QtyPurchaseItemsPerPurchase(long id);

        Purchase FindById(long id);

        bool PurchaseExists(long id);

        void Update(Purchase purchase);

        Purchase Add(Purchase purchase);

        void Remove(long id);

        void Save();
    }
}
