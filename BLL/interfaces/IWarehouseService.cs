using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IWarehouseService
    {
        List<Warehouse> GetAllWarehouses();

        List<SelectListItem> GetListLocationsIsWarehouse();

        Tuple<long, Warehouse, List<Asset>> GetWarehouseWithAssets(long warehouseID);

        Warehouse FindById(long id);

        bool WarehouseExists(long id);

        void Update(Warehouse warehouse);

        void Add(Warehouse warehouse);

        void Remove(long id);

        void Save();
    }
}
