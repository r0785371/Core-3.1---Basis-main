using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IPurchaseRepository
    {
        List<Purchase> GetAllPurchases();

        List<SelectListItem> GetSelectListPurchases();

        Purchase FindById(long id);

        List<Purchase> GetAllPurchasesOfSupplier(long supplierID);

        bool PurchaseExists(long id);

        void Update(Purchase purchase);

        Purchase Add(Purchase purchase);

        void Remove(long id);

        void Save();

        int QtyPurchasePerSupplier(long supplierID);
    }
}
