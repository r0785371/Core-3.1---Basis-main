using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAllSuppliers();

        List<SelectListItem> GetSelectListSuppliers();

        List<SelectListItem> GetSelectListSpecificSuppliers(string productChildren);

        Supplier FindById(long id);

        bool SupplierExists(long id);

        void Update(Supplier supplier);

        void Add(Supplier supplier);

        void Remove(long id);

        void Save();
    }
}
