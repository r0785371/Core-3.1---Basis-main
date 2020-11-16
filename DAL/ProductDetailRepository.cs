using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ProductDetailRepository: IProductDetailRepository
    {
        readonly DataContext context;

        public ProductDetailRepository(DataContext _context)
        {
            context = _context;
        }

        public List<ProductDetail> GetAllProductDetails()
        {
            return context.ProductDetails
                .Include(p => p.Product)
                .Include(p => p.Detail)
                .ThenInclude(p => p.DetailMain)
                .Include(p => p.Detail)
                .ThenInclude(p => p.DetailSub)
                .ToList();
        }

        public List<ProductDetail> GetAllProductDetailsPerProduct(long id)
        {
            return context.ProductDetails
                .Include(p => p.Product)
                .Include(p => p.Detail)
                .ThenInclude(p => p.DetailMain)
                .Include(p => p.Detail)
                .ThenInclude(p => p.DetailSub)
                .Where(s => s.ProductID == id)
                .ToList();
        }

        public List<SelectListItem> GetSelectListProductDetails()
        {
            return context.ProductDetails.Select(s => new SelectListItem
            {
                Value = s.ProductDetailID.ToString(),
                Text = s.Detail.DetailMain.Name + " " + s.Detail.DetailSub.Name,
                //Selected=c.ProductDetailID.Equals(1)
            }).ToList();
        }

        public List<ProductDetail> GetAllProductDetailsOfDetail(long detailID)
        {
            return context.ProductDetails
                .Where(p => p.DetailID == detailID)
                .Include(p => p.Detail)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .ToList();
        }

        public ProductDetail FindById(long id)
        {
            return context.ProductDetails
                .Where(s => s.ProductDetailID == id)
                .Include(p => p.Detail)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .Single();
        }

        public bool ProductDetailExists(long id)
        {
            return context.ProductDetails.Any(s => s.ProductDetailID == id);
        }

        public int QtyProductDetailPerProduct(long id)
        {
            return context.ProductDetails
                .Where(s => s.ProductID == id)
                .ToList()
                .Count();
        }

        public void Add(ProductDetail productDetail)
        {
            context.ProductDetails.Add(productDetail);
            context.SaveChanges();
        }

        public void Update(ProductDetail productDetail)
        {
            context.ProductDetails.Update(productDetail);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var productDetail = context.ProductDetails.SingleOrDefault(s => s.ProductDetailID == id);
            context.ProductDetails.Remove(productDetail);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
