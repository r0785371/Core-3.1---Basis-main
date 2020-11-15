using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class SupplierRepository: ISupplierRepository
    {
        readonly DataContext context;

        public SupplierRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Supplier> GetAllSuppliers()
        {
            return context.Suppliers
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListSuppliers()
        {
            return context.Suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public List<SelectListItem> GetSelectListSpecificSuppliers(string productChildren)
        {
            if (productChildren == "Hardware")
            {
                return context.Suppliers
                .Where(s => s.HasHardware == true)
                .Select(s => new SelectListItem
                {
                    Value = s.SupplierID.ToString(),
                    Text = s.Name,
                }).OrderBy(o => o.Text).ToList();
            }
            else if (productChildren == "Software")
            {
                return context.Suppliers
                   .Where(s => s.HasSoftware == true)
                   .Select(s => new SelectListItem
                   {
                       Value = s.SupplierID.ToString(),
                       Text = s.Name,
                }).OrderBy(o => o.Text).ToList();
            }
            return null;
            
        }

        public Supplier FindById(long id)
        {
            return context.Suppliers.Where(s => s.SupplierID == id).Single();
        }

        public bool SupplierExists(long id)
        {
            return context.Suppliers.Any(s => s.SupplierID == id);
        }

        public void Add(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            context.Suppliers.Update(supplier);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var supplier = context.Suppliers.SingleOrDefault(s => s.SupplierID == id);
            context.Suppliers.Remove(supplier);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
