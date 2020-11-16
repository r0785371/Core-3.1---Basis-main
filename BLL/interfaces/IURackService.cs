using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IURackService
    {
        List<URack> GetAllURacks();

        Tuple<long, URack, List<Asset>> GetAllURacksWithAssets(long uRackID);

        URack FindById(long id);

        bool URackExists(long id);

        void Update(URack uRack);

        void Add(URack uRack);

        void Remove(long id);

        void Save();
    }
}
