using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DAL
{
    public class DetailRepository: IDetailRepository
    {
        readonly DataContext context;

        public DetailRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Detail> GetAllDetails()
        {
            return context.Details
                .Include(d => d.DetailMain)
                .Include(d => d.DetailSub)
                .ToList();
        }

        public List<SelectListItem> GetSelectListDetails()
        {
            return context.Details.Select(s => new SelectListItem
            {
                Value = s.DetailID.ToString(),
                Text = s.DetailMain.Name + " " +s.DetailSub.Name,
                //Selected=c.DetailID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public List<SelectListItem> GetSelectListDetailsOfProductType(long productTypeID)
        {
            List<Detail> details = GetAllDetails();
            List<Detail> filteredDetails = new List<Detail>();

            foreach (var item in details)
            {
                if (item.SelectProductTypes != null)
                {
                    foreach (var productType in item.SelectProductTypes)
                    {
                        if (productType == productTypeID)
                        {
                            filteredDetails.Add(item);
                        }
                    }
                }
            }

            return filteredDetails
                .Select(s => new SelectListItem
                {
                    Value = s.DetailID.ToString(),
                    Text = s.DetailMain.Name + " " + s.DetailSub.Name,
                    //Selected = s.SelectProductTypes.Contains(productTypeID)
                }).OrderBy(o => o.Text).ToList();
        }

        public List<Detail> GetAllDetailsOfDetailMain(long detailMainID)
        {
            return context.Details
                .Where(d => d.DetailMainID == detailMainID)
                .Include(d => d.DetailMain)
                .Include(d => d.DetailSub)
                .ToList();
        }

        public List<Detail> GetAllDetailsOfDetailSub(long detailSubID)
        {
            return context.Details
                .Where(d => d.DetailSubID == detailSubID)
                .Include(d => d.DetailMain)
                .Include(d => d.DetailSub)
                .ToList();
        }

        public Detail FindById(long id)
        {
            return context.Details
                .Where(d => d.DetailID == id)
                .Include(d => d.DetailMain)
                .Include(d => d.DetailSub)
                .Single();
        }

        public bool DetailExists(long id)
        {
            return context.Details.Any(d => d.DetailID == id);
        }

        public void Add(Detail detail)
        {
            context.Details.Add(detail);
            context.SaveChanges();
        }

        public void Update(Detail detail)
        {
            context.Details.Update(detail);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var detail = context.Details.SingleOrDefault(d => d.DetailID == id);
            context.Details.Remove(detail);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
