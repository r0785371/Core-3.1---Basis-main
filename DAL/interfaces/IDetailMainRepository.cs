using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IDetailMainRepository
    {
        List<DetailMain> GetAllDetailMains();

        List<SelectListItem> GetSelectListDetailMains();

        DetailMain FindById(long id);

        bool DetailMainExists(long id);

        void Update(DetailMain detailParent);

        void Add(DetailMain detailParent);

        void Remove(long id);

        void Save();
    }
}
