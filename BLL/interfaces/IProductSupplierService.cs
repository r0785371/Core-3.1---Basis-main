using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IProductSupplierService
    {

        List<SelectListItem> GetSelectListAllSuppliers();

        ProductSupplier FindById(long id);

        bool ProductSupplierExists(long id);

        void Update(ProductSupplier productSupplier);

        void Add(ProductSupplier productSupplier);

        void Remove(long id);

        void Save();

        List<SelectListItem> GetSuppliersOfProductChild(string productChildren);
    }
}
