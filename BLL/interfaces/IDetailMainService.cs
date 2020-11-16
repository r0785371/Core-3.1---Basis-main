using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IDetailMainService
    {
        List<DetailMain> GetAllDetailMains();

        Tuple<long, DetailMain, List<Detail>> GetAllDetailMainWithDetails(long detailMainID);

        DetailMain FindById(long id);

        bool DetailMainExists(long id);

        void Update(DetailMain detailMain);

        void Add(DetailMain detailMain);

        void Remove(long id);

        void Save();
    }
}
