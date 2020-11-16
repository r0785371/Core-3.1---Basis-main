using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class WarehouseRepository: IWarehouseRepository
    {
        readonly DataContext context;

        public WarehouseRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Warehouse> GetAllWarehouses()
        {
            return context.Warehouses
                .Include(w => w.Location)
                        .ThenInclude(a => a.Building)
                .Include(w => w.Location)
                        .ThenInclude(a => a.Floor)
                .Include(w => w.Location)
                        .ThenInclude(a => a.Room)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListWarehouses()
        {
            return context.Warehouses.Select(s => new SelectListItem
            {
                Value = s.WarehouseID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public Warehouse FindById(long id)
        {
            return context.Warehouses
                .Where(w => w.WarehouseID == id)
                .Include(w => w.Location)
                        .ThenInclude(a => a.Building)
                .Include(w => w.Location)
                        .ThenInclude(a => a.Floor)
                .Include(w => w.Location)
                        .ThenInclude(a => a.Room)
                .Single();
        }

        public bool WarehouseExists(long id)
        {
            return context.Warehouses.Any(s => s.WarehouseID == id);
        }

        public long Add(Warehouse warehouse)
        {
            context.Warehouses.Add(warehouse);
            context.SaveChanges();
            return warehouse.WarehouseID;
        }

        public void Update(Warehouse warehouse)
        {
            context.Warehouses.Update(warehouse);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var warehouse = context.Warehouses.SingleOrDefault(s => s.WarehouseID == id);
            context.Warehouses.Remove(warehouse);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
