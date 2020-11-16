using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IWarningPeriodService
    {
        List<WarningPeriod> GetAllWarningPeriods();

        Tuple<long, WarningPeriod, List<Asset>, List<ProductType>> GetAllWarningPeriodsWithSubs(long warningPeriodID);

        WarningPeriod FindById(long id);

        bool WarningPeriodExists(long id);

        void Update(WarningPeriod warningPeriod);

        void Add(WarningPeriod warningPeriod);

        void Remove(long id);

        void Save();
    }
}
