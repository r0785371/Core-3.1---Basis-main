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
    public class ProductSupplierRepository: IProductSupplierRepository
    {
        readonly DataContext context;

        public ProductSupplierRepository(DataContext _context)
        {
            context = _context;
        }

        public List<SelectListItem> GetSelectListProductSuppliers()
        {
            return context.ProductSuppliers.Select(s => new SelectListItem
            {
                Value = s.ProductSupplierID.ToString(),
                Text = s.Supplier.Name + ": " + s.Product.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        //Denk dat deze niet nodig is...!
        public ProductSupplier FindById(long id)
        {
            return context.ProductSuppliers
                .Include(p => p.Supplier)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .Where(s => s.ProductSupplierID == id)
                .Single();
        }

        public List<ProductSupplier> GetAllProductSuppliersPerProduct(long id)
        {
            return context.ProductSuppliers
                .Where(s => s.ProductID == id)
                .Include(p => p.Supplier)
                .ToList();
        }

        public bool ProductSupplierExists(long id)
        {
            return context.ProductSuppliers.Any(s => s.ProductSupplierID == id);
        }

        public int QtyProductSupplierPerProduct(long id)
        {
            return context.ProductSuppliers
                .Where(s => s.ProductID == id)
                .ToList()
                .Count();
        }

        public void Add(ProductSupplier productSupplier)
        {
            context.ProductSuppliers.Add(productSupplier);
            context.SaveChanges();
        }

        public void Update(ProductSupplier productSupplier)
        {
            context.ProductSuppliers.Update(productSupplier);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var productSupplier = context.ProductSuppliers.SingleOrDefault(s => s.ProductSupplierID == id);
            context.ProductSuppliers.Remove(productSupplier);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int QtyProductSupplierPerSupplier(long supplierID)
        {
            return context.ProductSuppliers
                .Where(p => p.SupplierID == supplierID)
                .ToList()
                .Count();
        }
    }
}
