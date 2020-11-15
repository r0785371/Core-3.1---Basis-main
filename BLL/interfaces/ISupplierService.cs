using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface ISupplierService
    {
        List<Supplier> GetAllSuppliers();

        Supplier FindById(long id);

        //Tuple<long, Supplier, List<Hardware>, List<Software>, List<Purchase>> GetSupplierWithSub(long supplierID);

        bool SupplierExists(long id);

        void Update(Supplier supplier);

        void Add(Supplier supplier);

        void Remove(long id);

        void Save();

        //Tuple<long, Supplier, int, int> CanDeleteSupplier(long supplierID);
    }
}
