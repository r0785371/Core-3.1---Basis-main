using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL.interfaces
{
    public interface ISoftwareService
    {
        List<Software> GetAllSoftware();

        IEnumerable<Software> GetAllSoftwareEnum(string searchString);

        List<SelectListItem> GetSelectListProductTypeSoftware();

        List<SelectListItem> GetSelectListSoftwareTypes();

        List<SelectListItem> GetSelectListStatusProduct();

        Tuple<long, Software, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>> GetAllSoftwareOfProduct(long productID);

        Software FindById(long id);

        softwareViewModel LoadSoftwareViewModel(long id);

        bool SoftwareExists(long id);

        void Update(Software software);

        Software Add(Software software);

        Tuple<long, Software, int, int, int> CanDeleteSoftware(long productID);

        void Remove(long id);

        void Save();

        Tuple<string, string, bool> UploadProductPdf(long? productID, string FileDescription, IFormFile file);

        Tuple<string, FileStream, bool> GetSpecificationFilePdf(long productID);

        Tuple<string, string, bool> RemoveSpecificationFilePdf( long productID);
    }
}
