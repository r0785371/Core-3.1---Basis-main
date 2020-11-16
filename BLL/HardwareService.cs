using BLL.interfaces;
using BLL.ViewModels;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NinjaNye.SearchExtensions;
using System.Linq;

namespace BLL
{
    public class HardwareService : IHardwareService
    {
        readonly IHardwareRepository repository;
        readonly IProductTypeRepository repositoryProductType;
        readonly IProductSupplierRepository repositoryProductSupplier;
        readonly IProductDetailRepository repositoryProductDetail;
        readonly IPurchaseItemRepository repositoryPurchaseItem;
        readonly IStatusRepository repositoryStatus;
        readonly IDetailRepository repositoryDetail;
        private readonly IHostingEnvironment hostingEnv;

        public HardwareService(IHardwareRepository _repository, IProductTypeRepository _repositoryProductType,
        IProductSupplierRepository _repositoryProductSupplier, IProductDetailRepository _repositoryProductDetail,
        IPurchaseItemRepository _repositoryPurchaseItem, IStatusRepository _repositoryStatus,
        IDetailRepository _repositoryDetail, IHostingEnvironment _hostingEnv)
        {
            repository = _repository;
            repositoryProductType = _repositoryProductType;
            repositoryProductSupplier = _repositoryProductSupplier;
            repositoryProductDetail = _repositoryProductDetail;
            repositoryPurchaseItem = _repositoryPurchaseItem;
            repositoryStatus = _repositoryStatus;
            repositoryDetail = _repositoryDetail;
            hostingEnv = _hostingEnv;
        }

        public List<Hardware> GetAllHardware()
        {
            return repository.GetAllHardware();
        }

        public IEnumerable<Hardware> GetAllHardwareEnum(string searchString)
        {
            IEnumerable<Hardware> hardwares = repository.GetAllHardwareEnum();

            if (!string.IsNullOrEmpty(searchString))
            {
                //Installed nuget package: NinjaNye.SearchExtensions (http://ninjanye.github.io/SearchExtensions/stringsearch.html) which works really good!

                hardwares = hardwares.Search(h => h.Name ?? "",
                    h => h.Status != null ? h.Status.Name : "",
                    h => h.ProductType != null ? h.ProductType.Name : "",
                    h => h.Description ?? "",
                    h => h.SpecificationFileName ?? "")
                    .Containing(searchString);
            }

            return hardwares;

        }

        public IQueryable<Hardware> GetAllHardwareIQuery(string searchString)
        {
            IQueryable<Hardware> hardwares = repository.GetAllHardwareIQuery();

            if (!string.IsNullOrEmpty(searchString))
            {
                //Installed nuget package: NinjaNye.SearchExtensions (http://ninjanye.github.io/SearchExtensions/stringsearch.html) which works really good!

                hardwares = hardwares.Search(h => h.Name ?? "",
                    h => h.Status != null ? h.Status.Name : "",
                    h => h.ProductType != null ? h.ProductType.Name : "",
                    h => h.Description ?? "",
                    h => h.SpecificationFileName ?? "")
                    .Containing(searchString);
            }

            return hardwares;

        }

        public List<SelectListItem> GetSelectListProductTypeHardware()
        {
            return repositoryProductType.GetSelectListProductTypeHardware();
        }

        public List<SelectListItem> GetSelectListStatusProduct()
        {
            return repositoryStatus.GetSelectListStatusProduct();
        }

        public Hardware FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool HardwareExists(long id)
        {
            return repository.HardwareExists(id);
        }

        public void Update(Hardware hardware)
        {
            repository.Update(hardware);
        }

        public Hardware Add(Hardware hardware)
        {
            // Save hardware
            Hardware hardwareSaved = repository.Add(hardware);

            //Create the empty ProductDetails as pre-defined

            List<Detail> details = repositoryDetail.GetAllDetails();

            foreach (var detail in details)
            {
                if (detail.SelectProductTypes != null)
                {
                    foreach (var selectProductType in detail.SelectProductTypes)
                    {
                        if (selectProductType == hardwareSaved.ProductTypeID)
                        {
                            ProductDetail productDetail = new ProductDetail();

                            productDetail.ProductID = hardwareSaved.ProductID;
                            productDetail.DetailID = detail.DetailID;
                            productDetail.Definition1 = "";
                            productDetail.Definition2 = "";

                            repositoryProductDetail.Add(productDetail);
                        }
                    }
                }
            }

            return hardwareSaved;
        }

        public Tuple<long, Hardware, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>> GetAllHardwareOfProduct(long productID)
        {
            Hardware hardware = FindById(productID);

            List<ProductSupplier> productSuppliers = repositoryProductSupplier.GetAllProductSuppliersPerProduct(productID);

            List<ProductDetail> productDetails = repositoryProductDetail.GetAllProductDetailsPerProduct(productID);

            List<PurchaseItem> purchaseItems = repositoryPurchaseItem.GetAllPurchaseItemsOfProduct(productID);

            return new Tuple<long, Hardware, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>>(productID, hardware, productSuppliers, productDetails, purchaseItems);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public HardwareViewModel loadHardwareViewModel(long id)
        {
            HardwareViewModel hardwareViewModel = new HardwareViewModel();

            //Get the specific hardware (product) info...
            hardwareViewModel.hardware = FindById(id);
            hardwareViewModel.FileDescription = hardwareViewModel.hardware.SpecificationFileName;

            //and get then all suppliers for this hardware (product)...
            hardwareViewModel.productSuppliers = repositoryProductSupplier.GetAllProductSuppliersPerProduct(id);

            //and geet then all product details for this hardware (product).
            hardwareViewModel.productDetails = repositoryProductDetail.GetAllProductDetailsPerProduct(id);

            hardwareViewModel.purchaseItems = repositoryPurchaseItem.GetAllPurchaseItemsOfProduct(id);

            return hardwareViewModel;
        }

        public Tuple<string, string, bool> UploadProductPdf(long? productID, string FileDescription, IFormFile file)
        {
            Tuple<string, string, bool> oldFile = new Tuple<string, string, bool>("", "", false);
            string newFilePath = "";
            bool hasFile = false;

            //check then if there exist already one old file
            if (productID != 0)
            {
                oldFile = repository.GetSpecificationFilePathOfProduct(productID.Value);
            }

            if (file != null)
            {
                //Get the original file name
                var newFileName = Path.GetFileName(file.FileName);

                //Get the extension of the file
                string newFileExt = Path.GetExtension(file.FileName);

                //Get the new file path which is under wwwroot\hardwarePdf
                // Maybe a option to change the file name...
                newFilePath = Path.Combine(hostingEnv.WebRootPath, "hardwarePdf", newFileName);

                //Check if the file is not empty
                if (newFileName.Length > 0 && newFileExt.ToLower() == ".pdf")
                {

                    // If new file is different than local one, remove the old local PDF file.
                    if (newFilePath != oldFile.Item2)
                    {
                        // string fullPath = Request.MapPath("~/uploaded/" + file);
                        if (System.IO.File.Exists(oldFile.Item2))
                        {
                            System.IO.File.Delete(oldFile.Item2);
                        }

                    }

                    //Save the new file
                    using (var fileSteam = new FileStream(newFilePath, FileMode.Create))
                    {
                        file.CopyTo(fileSteam);
                    }

                    hasFile = true;

                }
                else
                {
                    return new Tuple<string, string, bool>(oldFile.Item1, oldFile.Item2, oldFile.Item3);
                }

            }
            else
            {
                return new Tuple<string, string, bool>(oldFile.Item1, oldFile.Item2, oldFile.Item3);
            }

            return new Tuple<string, string, bool>(FileDescription, newFilePath, hasFile);
        }


        ////OtherOption:
        ////This option will give / generate a new file name with extension.tmp
        //var filePath2 = Path.GetTempFileName();

        ////will save at the following location c:\users\xxx\AppData\Local\Temp
        //using (var stream = System.IO.File.Create(filePath2))
        //{
        //    hardwareViewModel.File.CopyTo(stream);
        //}


        public Tuple<string, FileStream, bool>  GetSpecificationFilePdf(long productID)
        {
            Tuple<string, FileStream, bool> emptyFile = new Tuple<string, FileStream, bool>("", null, false);
            FileStream stream = null;

            try
            {
                // will get the full file path from the database
                Tuple<string, string, bool> filePath = repository.GetSpecificationFilePathOfProduct(productID);
                if (!string.IsNullOrEmpty(filePath.Item2))
                {
                    // will get the pdf from wwwroot\hardwarePdf\ and send it to controler via FileStream
                    stream = new FileStream(filePath.Item2, FileMode.Open, FileAccess.Read);

                    if (stream != null)
                    {
                        return new Tuple<string, FileStream, bool>(filePath.Item1, stream, filePath.Item3);
                    }

                }
            }
            catch (Exception)
            {

                //throw;
                //return emptyFile;

            }

            return emptyFile;
        }

        public Tuple<string, string, bool> RemoveSpecificationFilePdf(long productID)
        {
            Tuple<string, string, bool> deletedFile = repository.RemoveSpecificationFilePdf(productID);

            return deletedFile;
        }

    }
}
