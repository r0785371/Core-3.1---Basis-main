using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface ISoftwareTypeService
    {
        List<SoftwareType> GetAllSoftwareTypes();

        Tuple<long, SoftwareType, List<Software>> GetAllSoftwareTypesWithSoftware(long softwareTypeID);

        SoftwareType FindById(long id);

        bool SoftwareTypeExists(long id);

        void Update(SoftwareType softwareType);

        void Add(SoftwareType softwareType);

        void Remove(long id);

        void Save();
    }
}
