using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IStatusService
    {
        List<Status> GetAllStatus();

        Status FindById(long id);

        bool StatusExists(long id);

        void Update(Status status);

        void Add(Status status);

        void Remove(long id);

        void Save();

        Tuple<long, Status, List<Hardware>, List<Software>, List<PurchaseItem>, List<License>, List<Asset>> GetStatusWithRelatedSubs(long statusID);
    }
}
