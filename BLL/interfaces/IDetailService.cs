using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IDetailService
    {
        List<Detail> GetAllDetails();

        List<SelectListItem> GetSelectListDetailMains();

        List<SelectListItem> GetSelectListDetailSubs();

        List<SelectListItem> GetSelectListProductTypes();

        Tuple<long, Detail, List<ProductDetail>, List<AssetDetail>> GetAllDetailsWithSub(long detailID);

        Detail FindById(long id);

        bool DetailExists(long id);

        void Update(Detail detail);

        void Add(Detail detail);

        void Remove(long id);

        void Save();
    }
}
