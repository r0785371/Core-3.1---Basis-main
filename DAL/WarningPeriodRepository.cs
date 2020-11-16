using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DAL
{
    public class WarningPeriodRepository: IWarningPeriodRepository
    {
        readonly DataContext context;

        public WarningPeriodRepository(DataContext _context)
        {
            context = _context;
        }

        public List<WarningPeriod> GetAllWarningPeriods()
        {
            return context.WarningPeriods
                .OrderBy(o => o.WarningPeriodMonth)
                .ToList();
        }

        public List<SelectListItem> GetSelectListWarningPeriods()
        {
            return context.WarningPeriods
                .OrderBy(o => o.WarningPeriodMonth)
                .Select(s => new SelectListItem
            {
                Value = s.WarningPeriodID.ToString(),
                Text = s.Name,
                //Selected=c.WarningPeriodID.Equals(1)
            }).ToList();
        }

        public WarningPeriod FindById(long id)
        {
            return context.WarningPeriods.Where(s => s.WarningPeriodID == id).Single();
        }

        public bool WarningPeriodExists(long id)
        {
            return context.WarningPeriods.Any(s => s.WarningPeriodID == id);
        }

        public void Add(WarningPeriod warningPeriod)
        {
            context.WarningPeriods.Add(warningPeriod);
            context.SaveChanges();
        }

        public void Update(WarningPeriod warningPeriod)
        {
            context.WarningPeriods.Update(warningPeriod);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var warningPeriod = context.WarningPeriods.SingleOrDefault(s => s.WarningPeriodID == id);
            context.WarningPeriods.Remove(warningPeriod);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
