using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL.interfaces
{
    public interface IHardwareRepository
    {
        List<Hardware> GetAllHardware();

        IEnumerable<Hardware> GetAllHardwareEnum();

        IQueryable<Hardware> GetAllHardwareIQuery();

        List<SelectListItem> GetSelectListHardware();

        List<Hardware> GetAllHardwareOfStatus(long statusID);

        List<Hardware> GetAllHardwareOFSupplier(long supplierID);

        Hardware FindById(long id);

        bool HardwareExists(long id);

        void Update(Hardware hardware);

        Hardware Add(Hardware hardware);

        void Remove(long id);

        void Save();

        Tuple<string, string, bool>  GetSpecificationFilePathOfProduct(long productID);

        Tuple<string, string, bool> RemoveSpecificationFilePdf(long productID);
    }
}
