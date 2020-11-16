using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IDetailRepository
    {
        List<Detail> GetAllDetails();

        List<SelectListItem> GetSelectListDetails();

        List<SelectListItem> GetSelectListDetailsOfProductType(long productTypeID);

        List<Detail> GetAllDetailsOfDetailMain(long detailMainID);

        List<Detail> GetAllDetailsOfDetailSub(long detailSubID);

        Detail FindById(long id);

        bool DetailExists(long id);

        void Update(Detail detail);

        void Add(Detail detail);

        void Remove(long id);

        void Save();
    }
}
