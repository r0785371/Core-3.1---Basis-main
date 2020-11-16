using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PurchaseTypeRepository: IPurchaseTypeRepository
    {
        readonly DataContext context;

        public PurchaseTypeRepository(DataContext _context)
        {
            context = _context;
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            return context.PurchaseTypes
                //.Include(a => a.WarningPeriod)
                .ToList();
        }

        public List<SelectListItem> GetSelectListPurchaseTypes()
        {
            return context.PurchaseTypes.Select(s => new SelectListItem
            {
                Value = s.PurchaseTypeID.ToString(),
                Text = s.Name,
                //Selected=c.PurchaseTypeID.Equals(1)
            }).ToList();
        }

        public PurchaseType FindById(long id)
        {
            return context.PurchaseTypes.Where(s => s.PurchaseTypeID == id).Single();
        }

        public bool PurchaseTypeExists(long id)
        {
            return context.PurchaseTypes.Any(s => s.PurchaseTypeID == id);
        }

        public void Add(PurchaseType purchaseType)
        {
            context.PurchaseTypes.Add(purchaseType);
            context.SaveChanges();
        }

        public void Update(PurchaseType purchaseType)
        {
            context.PurchaseTypes.Update(purchaseType);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var purchaseType = context.PurchaseTypes.SingleOrDefault(s => s.PurchaseTypeID == id);
            context.PurchaseTypes.Remove(purchaseType);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
