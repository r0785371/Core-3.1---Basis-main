using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL.interfaces
{
    public interface IHardwareService
    {
        List<Hardware> GetAllHardware();

        IEnumerable<Hardware> GetAllHardwareEnum(string searchString);

        IQueryable<Hardware> GetAllHardwareIQuery(string searchString);

        List<SelectListItem> GetSelectListProductTypeHardware();

        List<SelectListItem> GetSelectListStatusProduct();

        HardwareViewModel loadHardwareViewModel(long id);

        Hardware FindById(long id);

        bool HardwareExists(long id);

        void Update(Hardware hardware);

        Hardware Add(Hardware hardware);

        Tuple<long, Hardware, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>> GetAllHardwareOfProduct(long productID);

        void Remove(long id);

        void Save();

        Tuple<string, string, bool> UploadProductPdf(long? productID,string FileDescription, IFormFile file);

        Tuple<string, FileStream, bool> GetSpecificationFilePdf(long productID);

        // item1 = SpecificationFileName, item2 = SpecificationFilePath, item3 = HasFile. 
        Tuple<string, string, bool> RemoveSpecificationFilePdf(long productID);
    }
}
