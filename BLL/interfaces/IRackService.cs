using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
   public interface IRackService
    {
        List<Rack> GetAllRacks();

        Tuple<long, Rack, List<RackLocation>> GetAllRacksWithRackLocations(long rackID);

        Rack FindById(long id);

        bool RackExists(long id);

        void Update(Rack rack);

        void Add(Rack rack);

        void Remove(long id);

        void Save();
    }
}
