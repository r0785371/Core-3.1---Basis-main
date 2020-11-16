using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ISoftwareTypeRepository
    {
        List<SoftwareType> GetAllSoftwareTypes();

        List<SelectListItem> GetSelectListSoftwareTypes();

        SoftwareType FindById(long id);

        bool SoftwareTypeExists(long id);

        void Update(SoftwareType softwareType);

        void Add(SoftwareType softwareType);

        void Remove(long id);

        void Save();
    }
}
