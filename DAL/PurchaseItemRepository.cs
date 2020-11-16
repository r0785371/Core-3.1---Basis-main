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
    public class PurchaseItemRepository: IPurchaseItemRepository
    {
        readonly DataContext context;

        public PurchaseItemRepository(DataContext _context)
        {
            context = _context;
        }

        public List<PurchaseItem> GetAllPurchaseItems()
        {
            return context.PurchaseItems
                .Include(p => p.Product)
                .Include(p => p.Purchase)
                .Include(p => p.Status)
                .ToList();
        }

        public List<SelectListItem> GetSelectListPurchaseItems()
        {
            return context.PurchaseItems.Select(s => new SelectListItem
            {
                Value = s.PurchaseItemID.ToString(),
                Text = s.Product.Name + " - " + s.Purchase.Supplier.Name + " - No: " + s.Purchase.No + " - " + s.DeliveryDate.ToShortDateString(),
                //Selected=c.PurchaseItemID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public List<PurchaseItem> GetAllPurchaseItemsPerPurchase(long id)
        {
            return context.PurchaseItems
                .Where(s => s.PurchaseID == id)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .Include(p => p.Purchase)
                .Include(p => p.Status)
                .ToList();
        }

        public List<PurchaseItem> GetAllPurchaseItemsOfProduct(long productID)
        {
            return context.PurchaseItems
                .Where(s => s.ProductID == productID)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .Include(p => p.Purchase)
                .Include(p => p.Status)
                .ToList();
        }

        public List<PurchaseItem> GetAllPurchaseItemsOfStatus(long statusID)
        {
            return context.PurchaseItems
                .Where(p => p.StatusID == statusID)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .Include(p => p.Purchase)
                .Include(p => p.Status)
                .OrderBy(p => p.Purchase.No)
                .ToList();
        }

        public int QtyPurchaseItemsPerPurchase(long id)
        {
            return context.PurchaseItems
                .Where(s => s.PurchaseID == id)
                .ToList()
                .Count();
        }

        public int QtyPurchaseItemPerProduct(long productID)
        {
            return context.PurchaseItems
                .Where(p => p.ProductID == productID)
                .ToList()
                .Count();
        }

        public PurchaseItem FindById(long id)
        {
            return context.PurchaseItems
                .Where(s => s.PurchaseItemID == id)
                .Include(p => p.Product)
                    .ThenInclude(p => p.ProductType)
                    .ThenInclude(p => p.Products)
                .Include(p => p.Purchase)
                    .ThenInclude(p => p.Supplier)
                .Include(p => p.Status)
                .Single();
        }

        public bool PurchaseItemExists(long id)
        {
            return context.PurchaseItems.Any(s => s.PurchaseItemID == id);
        }

        public PurchaseItem Add(PurchaseItem purchaseItem)
        {
            context.PurchaseItems.Add(purchaseItem);
            context.SaveChanges();
            return purchaseItem;
        }

        public void Update(PurchaseItem purchaseItem)
        {
            context.PurchaseItems.Update(purchaseItem);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var purchaseItem = context.PurchaseItems.SingleOrDefault(s => s.PurchaseItemID == id);
            context.PurchaseItems.Remove(purchaseItem);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<PurchaseItem> GetListPurchaseItemsToOrder()
        {
            return context.PurchaseItems
                .Where(p => p.Status.ToOrder == true)
                .Include(p => p.Product)
                    .ThenInclude(p => p.ProductType)
                .Include(p => p.Purchase)
                    .ThenInclude(p => p.Supplier)
                .Include(p => p.Status)
                .ToList();
        }

        public List<PurchaseItem> GetListPurchaseItemsOrdered()
        {
            return context.PurchaseItems
                .Where(p => p.Status.Ordered == true)
                .Include(p => p.Product)
                    .ThenInclude(p => p.ProductType)
                .Include(p => p.Purchase)
                    .ThenInclude(p => p.Supplier)
                .Include(p => p.Status)
                .ToList();
        }

    }
}
