using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
   public interface IPurchaseItemService
    {
        List<PurchaseItem> GetAllPurchaseItems();

        List<SelectListItem> GetSelectListProducts();

        List<SelectListItem> GetSelectListActiveProducts();

        List<SelectListItem> GetSelectListProductsOfSupplier(long purchaseID,bool? hasHardware, bool? hasSoftware);

        List<SelectListItem> GetSelectListStatusPurchase();

        List<ProductType> GetAllProductTypes();

        Tuple<long, PurchaseItem, List<Asset>, List<License>> GetPurchaseItemWithSub(long purchaseItemID);

        PurchaseItem FindById(long id);

        bool PurchaseItemExists(long id);

        Tuple<string, int> Update(PurchaseItem purchaseItem);

        Tuple<string, int> Add(PurchaseItem purchaseItem);

        void Remove(long id);

        void Save();

        List<PurchaseItem> GetListPurchaseItemsToOrder();

        List<PurchaseItem> GetListPurchaseItemsOrdered();
    }
}
