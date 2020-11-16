using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IProductTypeService
    {
        List<ProductType> GetAllProductTypes();

        List<SelectListItem> GetSelectListProductTypeGroups();

        List<SelectListItem> GetSelectListWarningPeriods();

        ProductType FindById(long id);

        bool ProductTypeExists(long id);

        void Update(ProductType productType);

        void Add(ProductType productType);

        Tuple<long, ProductType, List<Product>> GetProductTypeWithProducts(long productTypeID);

        void Remove(long id);

        void Save();
    }
}
