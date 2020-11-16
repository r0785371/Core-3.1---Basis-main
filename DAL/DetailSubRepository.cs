using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class DetailSubRepository: IDetailSubRepository
    {
        readonly DataContext context;

        public DetailSubRepository(DataContext _context)
        {
            context = _context;
        }

        public List<DetailSub> GetAllDetailSubs()
        {
            return context.DetailSubs
                //.Include(a => a.WarningPeriod)
                .ToList();
        }

        public List<SelectListItem> GetSelectListDetailSubs()
        {
            return context.DetailSubs.Select(s => new SelectListItem
            {
                Value = s.DetailSubID.ToString(),
                Text = s.Name,
                //Selected=c.DetailSubID.Equals(1)
            }).ToList();
        }

        public DetailSub FindById(long id)
        {
            return context.DetailSubs.Where(s => s.DetailSubID == id).Single();
        }

        public bool DetailSubExists(long id)
        {
            return context.DetailSubs.Any(s => s.DetailSubID == id);
        }

        public void Add(DetailSub detailChild)
        {
            context.DetailSubs.Add(detailChild);
            context.SaveChanges();
        }

        public void Update(DetailSub detailChild)
        {
            context.DetailSubs.Update(detailChild);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var detailChild = context.DetailSubs.SingleOrDefault(s => s.DetailSubID == id);
            context.DetailSubs.Remove(detailChild);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
