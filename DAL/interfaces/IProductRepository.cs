using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        List<Product> GetAllProductsOfProductType(long productTypeID);

        List<SelectListItem> GetSelectListProducts();

        List<SelectListItem> GetSelectListActiveProducts();

        List<SelectListItem> GetSelectListProductsOfSupplier(long supplierID,bool? hasHardware, bool? hasSoftware);

        Product FindById(long id);

        bool ProductExists(long id);

        void Update(Product product);

        void Add(Product product);

        void Remove(long id);

        void Save();
    }
}
