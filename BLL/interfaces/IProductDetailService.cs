using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IProductDetailService
    {
        List<ProductDetail> GetAllProductDetails();

        List<SelectListItem> GetSelectListDetails();

        List<SelectListItem> GetSelectListDetailsOfProductType(long productID);

        ProductDetail FindById(long id);

        bool ProductDetailExists(long id);

        void Update(ProductDetail productDetail);

        void Add(ProductDetail productDetail);

        void Remove(long id);

        void Save();
    }
}
