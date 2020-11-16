using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        List<SelectListItem> GetSelectListProductTypes();

        List<SelectListItem> GetSelectListStatusProduct();

        Product FindById(long id);

        bool ProductExists(long id);

        void Update(Product product);

        void Add(Product product);

        void Remove(long id);

        void Save();
    }
}
