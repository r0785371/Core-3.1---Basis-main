using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IExternCompanyService
    {
        List<ExternCompany> GetAllExternCompanies();

        List<SelectListItem> GetSelectListExternCompanies();

        Tuple<long, ExternCompany, List<Asset>> GetExternCompanyWithSub(long externCompanyID);

        ExternCompany FindById(long id);

        bool ExternCompanyExists(long id);

        void Update(ExternCompany externCompany);

        void Add(ExternCompany externCompany);

        void Remove(long id);

        void Save();
    }
}
