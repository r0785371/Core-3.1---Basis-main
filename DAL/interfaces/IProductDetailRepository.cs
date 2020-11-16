using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IProductDetailRepository
    {
        List<ProductDetail> GetAllProductDetails();

        List<ProductDetail> GetAllProductDetailsPerProduct(long id);

        List<SelectListItem> GetSelectListProductDetails();

        List<ProductDetail> GetAllProductDetailsOfDetail(long detailID);

        ProductDetail FindById(long id);

        bool ProductDetailExists(long id);

        int QtyProductDetailPerProduct(long id);

        void Update(ProductDetail productDetail);

        void Add(ProductDetail productDetail);

        void Remove(long id);

        void Save();
    }
}
