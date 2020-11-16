using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IProductSupplierRepository
    {

        List<ProductSupplier> GetAllProductSuppliersPerProduct(long id);

        List<SelectListItem> GetSelectListProductSuppliers();

        ProductSupplier FindById(long id);

        int QtyProductSupplierPerProduct(long id);

        bool ProductSupplierExists(long id);

        void Update(ProductSupplier productSupplier);

        void Add(ProductSupplier productSupplier);

        void Remove(long id);

        void Save();

        int QtyProductSupplierPerSupplier(long supplierID);
    }
}
