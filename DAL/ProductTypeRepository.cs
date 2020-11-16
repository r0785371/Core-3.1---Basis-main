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
    public class ProductTypeRepository: IProductTypeRepository
    {
        readonly DataContext context;

        public ProductTypeRepository(DataContext _context)
        {
            context = _context;
        }

        public List<ProductType> GetAllProductTypes()
        {
            return context.ProductTypes
                .Include(p => p.WarningPeriod)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListProductTypeGroups()
        {
            return context.ProductTypes
                .Where(s => s.IsGroup == true)
                .Select(s => new SelectListItem
            {
                Value = s.ProductTypeID.ToString(),
                Text = s.Name,
                
            }).OrderBy(o => o.Text).ToList();

        }

        public List<SelectListItem> GetSelectListProductTypes()
        {
            return context.ProductTypes
                //.Where(s => s.IsLicense==true)
                .Select(s => new SelectListItem
            {
                //Selected = !s.IsLicense == true,
                //Disabled = !s.IsLicense,
                Value = s.ProductTypeID.ToString(),
                Text = s.Name,
                
            }).OrderBy(o => o.Text).ToList();
        }

        public List<SelectListItem> GetSelectListProductTypeHardware()
        {
            //List<SelectListItem> productTypes =

            return context.ProductTypes
               .Where(s => s.IsAsset == true || s.IsProduct == true || s.IsComponent == true)
               .Select(s => new SelectListItem
               {
                   Value = s.ProductTypeID.ToString(),
                   Text = s.Name,

               }).OrderBy(o => o.Text).ToList();

        }

        public List<ProductType> GetAllProductTypesOfWarningPeriod(long warningPeriodID)
        {
            return context.ProductTypes
                .Where(p => p.WarningPeriodID == warningPeriodID)
                .Include(p => p.WarningPeriod)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListProductTypeSoftware()
        {
            //List<SelectListItem> productTypes =
                
                return context.ProductTypes
                   .Where(s => s.IsLicense == true)
                   .Select(s => new SelectListItem
                   {
                       Value = s.ProductTypeID.ToString(),
                       Text = s.Name,

                   }).OrderBy(o => o.Text).ToList();

                    //var productTypesNull = new SelectListItem()
                    //{
                    //    Value = null,
                    //    Text = "--- select Product type ---"
                    //};
                    //productTypes.Insert(0, productTypesNull);
                    //return productTypes;
        }

        public ProductType FindById(long id)
        {
            return context.ProductTypes
                .Where(s => s.ProductTypeID == id)
                .Include(p => p.WarningPeriod)
                .Single();
        }

        public bool ProductTypeExists(long id)
        {
            return context.ProductTypes.Any(s => s.ProductTypeID == id);
        }

        public void Add(ProductType productType)
        {
            context.ProductTypes.Add(productType);
            context.SaveChanges();
        }

        public void Update(ProductType productType)
        {
            context.ProductTypes.Update(productType);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var productType = context.ProductTypes.SingleOrDefault(s => s.ProductTypeID == id);
            context.ProductTypes.Remove(productType);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
