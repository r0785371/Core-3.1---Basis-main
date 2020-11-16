using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IPurchaseItemRepository
    {
        List<PurchaseItem> GetAllPurchaseItems();

        List<SelectListItem> GetSelectListPurchaseItems();

        List<PurchaseItem> GetAllPurchaseItemsPerPurchase(long id);

        List<PurchaseItem> GetAllPurchaseItemsOfProduct(long productID);

        List<PurchaseItem> GetAllPurchaseItemsOfStatus(long statusID);

        int QtyPurchaseItemsPerPurchase(long id);

        int QtyPurchaseItemPerProduct(long productID);

        PurchaseItem FindById(long id);

        bool PurchaseItemExists(long id);

        void Update(PurchaseItem purchaseItem);

        PurchaseItem Add(PurchaseItem purchaseItem);

        void Remove(long id);

        void Save();

        List<PurchaseItem> GetListPurchaseItemsToOrder();

        List<PurchaseItem> GetListPurchaseItemsOrdered();
    }
}
