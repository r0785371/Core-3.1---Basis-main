using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IDetailSubService
    {
        List<DetailSub> GetAllDetailSubs();

        Tuple<long, DetailSub, List<Detail>> GetAllDetailSubWithDetails(long detailSubID);

        DetailSub FindById(long id);

        bool DetailSubExists(long id);

        void Update(DetailSub detailSub);

        void Add(DetailSub detailSub);

        void Remove(long id);

        void Save();
    }
}
