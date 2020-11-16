using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PurchaseRepository: IPurchaseRepository
    {
        readonly DataContext context;

        public PurchaseRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Purchase> GetAllPurchases()
        {
            return context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseType)
                .Include(p => p.PurchaseItems)
                .ToList();
        }

        public List<SelectListItem> GetSelectListPurchases()
        {
            return context.Purchases.Select(s => new SelectListItem
            {
                Value = s.PurchaseID.ToString(),
                Text = s.No + " " + s.Supplier.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public Purchase FindById(long id)
        {
            return context.Purchases
                .Where(s => s.PurchaseID == id)
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseType)
                .Include(p => p.PurchaseItems)
                .Single();
        }

        public List<Purchase> GetAllPurchasesOfSupplier(long supplierID)
        {
            return context.Purchases
                .Where(p => p.SupplierID == supplierID)
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseType)
                .Include(p => p.PurchaseItems)
                .ToList();
        }

        public bool PurchaseExists(long id)
        {
            return context.Purchases.Any(s => s.PurchaseID == id);
        }

        public Purchase Add(Purchase purchase)
        {
            context.Purchases.Add(purchase);
            context.SaveChanges();
            return purchase;
        }

        public void Update(Purchase purchase)
        {
            context.Purchases.Update(purchase);
            context.SaveChanges();
            
        }

        public void Remove(long id)
        {
            var purchase = context.Purchases.SingleOrDefault(s => s.PurchaseID == id);
            context.Purchases.Remove(purchase);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int QtyPurchasePerSupplier(long supplierID)
        {
            return context.Purchases
                .Where(p => p.SupplierID == supplierID)
                .ToList()
                .Count();
        }
    }
}
