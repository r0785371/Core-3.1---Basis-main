using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IExternCompanyRepository
    {
        List<ExternCompany> GetAllExternCompanies();

        List<SelectListItem> GetSelectListExternCompanies();

        ExternCompany FindById(long id);

        bool ExternCompanyExists(long id);

        void Update(ExternCompany externCompany);

        void Add(ExternCompany externCompany);

        void Remove(long id);

        void Save();
    }
}
