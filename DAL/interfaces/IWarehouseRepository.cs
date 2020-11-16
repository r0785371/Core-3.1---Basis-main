using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IWarehouseRepository
    {
        List<Warehouse> GetAllWarehouses();

        List<SelectListItem> GetSelectListWarehouses();

        Warehouse FindById(long id);

        bool WarehouseExists(long id);

        void Update(Warehouse warehouse);

        long Add(Warehouse warehouse);

        void Remove(long id);

        void Save();
    }
}
