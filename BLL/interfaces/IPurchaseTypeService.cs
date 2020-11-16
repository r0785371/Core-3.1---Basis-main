using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IPurchaseTypeService
    {
        List<PurchaseType> GetAllPurchaseTypes();

        PurchaseType FindById(long id);

        bool PurchaseTypeExists(long id);

        void Update(PurchaseType purchaseType);

        void Add(PurchaseType purchaseType);

        void Remove(long id);

        void Save();
    }
}
