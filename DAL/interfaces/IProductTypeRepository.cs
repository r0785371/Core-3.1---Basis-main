using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IProductTypeRepository
    {
        List<ProductType> GetAllProductTypes();

        List<SelectListItem> GetSelectListProductTypes();

        List<SelectListItem> GetSelectListProductTypeGroups();

        List<SelectListItem> GetSelectListProductTypeSoftware();

        List<SelectListItem> GetSelectListProductTypeHardware();

        List<ProductType> GetAllProductTypesOfWarningPeriod(long warningPeriodID);

        ProductType FindById(long id);

        bool ProductTypeExists(long id);

        void Update(ProductType productType);

        void Add(ProductType productType);

        void Remove(long id);

        void Save();
    }
}
