using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ISoftwareRepository
    {
        List<Software> GetAllSoftware();

        IEnumerable<Software> GetAllSoftwareEnum();

        List<SelectListItem> GetSelectListSoftware();

        List<Software> GetAllSoftwareOfStatus(long statusID);

        List<Software> GetAllSoftwareOfSoftwareType(long softwareTypeID);

        Software FindById(long id);

        List<Software> GetAllSoftwareOfSupplier(long supplierID);

        bool SoftwareExists(long id);

        void Update(Software software);

        Software Add(Software software);

        void Remove(long id);

        void Save();

        Tuple<string, string, bool> GetSpecificationFilePathOfProduct(long productID);

        Tuple<string, string, bool> RemoveSpecificationFilePdf(long productID);
    }
}
