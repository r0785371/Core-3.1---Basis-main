using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IDetailSubRepository
    {
        List<DetailSub> GetAllDetailSubs();

        List<SelectListItem> GetSelectListDetailSubs();

        DetailSub FindById(long id);

        bool DetailSubExists(long id);

        void Update(DetailSub detailChild);

        void Add(DetailSub detailChild);

        void Remove(long id);

        void Save();
    }
}
