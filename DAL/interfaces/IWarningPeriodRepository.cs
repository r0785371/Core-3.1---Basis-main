using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IWarningPeriodRepository
    {
        List<WarningPeriod> GetAllWarningPeriods();

        List<SelectListItem> GetSelectListWarningPeriods();

        WarningPeriod FindById(long id);

        bool WarningPeriodExists(long id);

        void Update(WarningPeriod warningPeriod);

        void Add(WarningPeriod warningPeriod);

        void Remove(long id);

        void Save();
    }
}
