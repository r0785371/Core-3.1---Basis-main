using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ExternCompanyRepository : IExternCompanyRepository
    {
        readonly DataContext context;

        public ExternCompanyRepository(DataContext _context)
        {
            context = _context;
        }

        public List<ExternCompany> GetAllExternCompanies()
        {
            return context.ExternCompanies
                .ToList();
        }

        public List<SelectListItem> GetSelectListExternCompanies()
        {
            return context.ExternCompanies.Select(s => new SelectListItem
            {
                Value = s.ExternCompanyID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public ExternCompany FindById(long id)
        {
            return context.ExternCompanies
                .Where(d => d.ExternCompanyID == id)
                .Single();
        }

        public bool ExternCompanyExists(long id)
        {
            return context.ExternCompanies.Any(d => d.ExternCompanyID == id);
        }

        public void Add(ExternCompany externCompany)
        {
            context.ExternCompanies.Add(externCompany);
            context.SaveChanges();
        }

        public void Update(ExternCompany externCompany)
        {
            context.ExternCompanies.Update(externCompany);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var detail = context.ExternCompanies.SingleOrDefault(d => d.ExternCompanyID == id);
            context.ExternCompanies.Remove(detail);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
