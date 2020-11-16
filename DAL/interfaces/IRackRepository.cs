using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IRackRepository
    {
        List<Rack> GetAllRacks();

        List<SelectListItem> GetSelectListRacks();

        Rack FindById(long id);

        bool RackExists(long id);

        void Update(Rack rack);

        void Add(Rack rack);

        void Remove(long id);

        void Save();
    }
}
