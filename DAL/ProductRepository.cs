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
    public class ProductRepository: IProductRepository
    {
        readonly DataContext context;

        public ProductRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Product> GetAllProducts()
        {
            return context.Products
                .Include(s => s.Status)
                .Include(p => p.ProductType)
                .ToList();
        }

        public List<Product> GetAllProductsOfProductType(long productTypeID)
        {
            return context.Products
                .Where(p => p.ProductTypeID == productTypeID)
                .Include(s => s.Status)
                .Include(p => p.ProductType)
                .ToList();
        }

        public List<SelectListItem> GetSelectListProducts()
        {
            return context.Products
                .Select(s => new SelectListItem
            {
                Value = s.ProductID.ToString(),
                Text = s.Name,
            }).ToList();
        }


        public List<SelectListItem> GetSelectListActiveProducts()
        {
            return context.Products
                .Include(s => s.Status)
                .Where(s => s.Status.NoSupport != true && s.Status.OutOfUse != true)
                .Select(s => new SelectListItem
                {
                    Value = s.ProductID.ToString(),
                    Text = s.Name,
                    
                }).ToList();
        }

        public List<SelectListItem> GetSelectListProductsOfSupplier(long supplierID, bool? hasHardware, bool? hasSoftware)
        {
            if (hasHardware == true)
            {
                return context.Products
                .Include(s => s.Status)
                .Include(s => s.ProductSuppliers)
                .Where(s => s.ProductSuppliers.Any(p => p.SupplierID == supplierID) && s.ProductType.ProductChild.ToString() == "Hardware")
                .Select(s => new SelectListItem
                {
                    Value = s.ProductID.ToString(),
                    Text = s.Name,
                }).ToList();
            }

            if (hasSoftware == true)
            {
                return context.Products
                .Include(s => s.Status)
                .Include(s => s.ProductSuppliers)
                .Where(s => s.ProductSuppliers.Any(p => p.SupplierID == supplierID) && s.ProductType.ProductChild.ToString() == "Software")
                .Select(s => new SelectListItem
                {
                    Value = s.ProductID.ToString(),
                    Text = s.Name,
                }).ToList();
            }

            return context.Products
                .Include(s => s.Status)
                .Include(s => s.ProductSuppliers)
                .Where(s => s.ProductSuppliers.Any(p => p.SupplierID == supplierID))
                .Select(s => new SelectListItem
                {
                    Value = s.ProductID.ToString(),
                    Text = s.Name,
                }).ToList();
        }

        public Product FindById(long id)
        {
            return context.Products.Where(s => s.ProductID == id).Single();
        }

        public bool ProductExists(long id)
        {
            return context.Products.Any(s => s.ProductID == id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var product = context.Products.SingleOrDefault(s => s.ProductID == id);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
