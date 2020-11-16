using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class DetailMainRepository: IDetailMainRepository
    {
        readonly DataContext context;

        public DetailMainRepository(DataContext _context)
        {
            context = _context;
        }

        public List<DetailMain> GetAllDetailMains()
        {
            return context.DetailMains
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListDetailMains()
        {
            return context.DetailMains.Select(s => new SelectListItem
            {
                Value = s.DetailMainID.ToString(),
                Text = s.Name,
                
                //Selected=c.DetailMainID.Equals(1)
            }).OrderBy(o => o.Text).ToList();
        }

        public DetailMain FindById(long id)
        {
            return context.DetailMains.Where(s => s.DetailMainID == id).Single();
        }

        public bool DetailMainExists(long id)
        {
            return context.DetailMains.Any(s => s.DetailMainID == id);
        }

        public void Add(DetailMain detailParent)
        {
            context.DetailMains.Add(detailParent);
            context.SaveChanges();
        }

        public void Update(DetailMain detailParent)
        {
            context.DetailMains.Update(detailParent);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var detailParent = context.DetailMains.SingleOrDefault(s => s.DetailMainID == id);
            context.DetailMains.Remove(detailParent);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
