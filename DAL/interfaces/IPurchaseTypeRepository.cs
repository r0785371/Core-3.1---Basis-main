using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IPurchaseTypeRepository
    {
        List<PurchaseType> GetAllPurchaseTypes();

        List<SelectListItem> GetSelectListPurchaseTypes();

        PurchaseType FindById(long id);

        bool PurchaseTypeExists(long id);

        void Update(PurchaseType purchaseType);

        void Add(PurchaseType purchaseType);

        void Remove(long id);

        void Save();
    }
}
