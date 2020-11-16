using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IURackRepository
    {
        List<URack> GetAllURacks();

        List<SelectListItem> GetSelectListURacks();

        URack FindById(long id);

        bool URackExists(long id);

        void Update(URack uRack);

        void Add(URack uRack);

        void Remove(long id);

        void Save();
    }
}
