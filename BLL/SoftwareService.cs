using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using BLL.interfaces;
using BLL.ViewModels;
using DAL.interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NinjaNye.SearchExtensions;

namespace BLL
{
    public class SoftwareService : ISoftwareService
    {
        readonly ISoftwareRepository repository;
        readonly IProductTypeRepository repositoryProductType;
        readonly ISoftwareTypeRepository repositorySoftwareType;
        readonly IProductSupplierRepository repositoryProductSupplier;
        readonly IProductDetailRepository repositoryProductDetail;
        readonly IPurchaseItemRepository repositoryPurchaseItem;
        readonly IStatusRepository repositoryStatus;
        private readonly IHostingEnvironment hostingEnv;

        public SoftwareService(ISoftwareRepository _repository, IProductTypeRepository _repositoryProductType,
        ISoftwareTypeRepository _repositorySoftwareType, IProductSupplierRepository _repositoryProductSupplier,
        IProductDetailRepository _repositoryProductDetail, IPurchaseItemRepository _repositoryPurchaseItem, IStatusRepository _repositoryStatus, IHostingEnvironment _hostingEnv)
        {
            repository = _repository;
            repositoryProductType = _repositoryProductType;
            repositorySoftwareType = _repositorySoftwareType;
            repositoryProductSupplier = _repositoryProductSupplier;
            repositoryProductDetail = _repositoryProductDetail;
            repositoryPurchaseItem = _repositoryPurchaseItem;
            repositoryStatus = _repositoryStatus;
            hostingEnv = _hostingEnv;
        }

        public IEnumerable<Software> GetAllSoftwareEnum(string searchString)
        {
            IEnumerable<Software> softwares = repository.GetAllSoftwareEnum();

            if (!string.IsNullOrEmpty(searchString))
            {
                //Installed nuget package: NinjaNye.SearchExtensions (http://ninjanye.github.io/SearchExtensions/stringsearch.html) which works really good!

                softwares = softwares.Search(h => h.Name ?? "",
                    h => h.Status != null ? h.Status.Name : "",
                    h => h.ProductType != null ? h.ProductType.Name : "",
                    h => h.SoftwareType != null ? h.SoftwareType.Name : "",
                    h => h.Description ?? "")
                    .Containing(searchString);
            }

            return softwares;
        }

        public List<Software> GetAllSoftware()
        {
            return repository.GetAllSoftware();
        }

        public List<SelectListItem> GetSelectListProductTypeSoftware()
        {
            return repositoryProductType.GetSelectListProductTypeSoftware();
        }

        public List<SelectListItem> GetSelectListStatusProduct()
        {
            return repositoryStatus.GetSelectListStatusProduct();
        }

        public List<SelectListItem> GetSelectListSoftwareTypes()
        {
            return repositorySoftwareType.GetSelectListSoftwareTypes();
        }

        public Tuple<long, Software, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>> GetAllSoftwareOfProduct(long productID)
        {
            Software software = FindById(productID);

            List<ProductSupplier> productSuppliers = repositoryProductSupplier.GetAllProductSuppliersPerProduct(productID);

            List<ProductDetail> productDetails = repositoryProductDetail.GetAllProductDetailsPerProduct(productID);

            List<PurchaseItem> purchaseItems = repositoryPurchaseItem.GetAllPurchaseItemsOfProduct(productID); 

            return new Tuple<long, Software, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>>(productID, software, productSuppliers, productDetails, purchaseItems);
        }

        public Software FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool SoftwareExists(long id)
        {
            return repository.SoftwareExists(id);
        }

        public void Update(Software software)
        {
            repository.Update(software);
        }

        public Software Add(Software software)
        {
            return repository.Add(software);
        }

        public Tuple<long, Software, int, int, int> CanDeleteSoftware(long productID)
        {
            Software software = FindById(productID);

            //Check qty of ProductSuppliers per specific Software (product)!
            int qtyProductSupplier = repositoryProductSupplier.QtyProductSupplierPerProduct(productID);

            //Check qty of ProductDetails per specific Software (product)!
            int qtyProductDetail = repositoryProductDetail.QtyProductDetailPerProduct(productID);

            //Check qty of PurchaseItems per specific Software (product)!
            int qtyPurchaseItem = repositoryPurchaseItem.QtyPurchaseItemPerProduct(productID);

            return new Tuple<long, Software, int, int, int>(productID, software, qtyProductSupplier, qtyProductDetail, qtyPurchaseItem);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }

        public softwareViewModel LoadSoftwareViewModel(long id)
        {
            softwareViewModel softwareViewModel = new softwareViewModel();

            //Get the specific software (product) info...
            softwareViewModel.software = FindById(id);
            softwareViewModel.FileDescription = softwareViewModel.software.SpecificationFileName;

            //and get then all suppliers for this software (product)...
            softwareViewModel.productSuppliers = repositoryProductSupplier.GetAllProductSuppliersPerProduct(id);

            //and geet then all product details for this software (product).
            softwareViewModel.productDetails = repositoryProductDetail.GetAllProductDetailsPerProduct(id);

            softwareViewModel.purchaseItems = repositoryPurchaseItem.GetAllPurchaseItemsOfProduct(id);

            return softwareViewModel;
        }

        //Not necessary - can be deleted????
        public softwareViewModel AddSupplierToSoftware(long productID, long supplierID, string refSupplier, string productNameSupplier, double price)
        {
            //Software software = null;
            if (productID == 0)
            {
                ////Create one new Software + Add to DB + Get ProductID
                //software = new Software();
                //software.ProductID = productID;

                //message that you need first to fill a product

            }

            // Add supplier to Software + add to DB
            ProductSupplier productSupplier = new ProductSupplier();

            productSupplier.ProductID = productID;
            productSupplier.SupplierID = supplierID;
            productSupplier.RefSupplier = refSupplier;
            productSupplier.ProductNameSupplier = productNameSupplier;
            productSupplier.Price = price;

            softwareViewModel softwareViewModel = new softwareViewModel();
            //softwareViewModel.software = software;
            softwareViewModel.productSuppliers.Add(productSupplier);

            return softwareViewModel;
        }

        //Not necessary - can be deleted????
        public softwareViewModel AddProductDetailToSoftware(long productID, long detailID, string definition1, string definition2)
        {
            //Software software = null;
            if (productID == 0)
            {
                ////Create one new Software + Add to DB + Get ProductID
                //software = new Software();
                //software.ProductID = productID;

                //message that you need first to fill a product

            }

            // Add supplier to Software + add to DB
            ProductDetail productDetail = new ProductDetail();

            productDetail.ProductID = productID;
            productDetail.DetailID = detailID;
            productDetail.Definition1 = definition1;
            productDetail.Definition2 = definition2;


            softwareViewModel softwareViewModel = new softwareViewModel();
            //softwareViewModel.software = software;
            softwareViewModel.productDetails.Add(productDetail);

            return softwareViewModel;
        }

        public Tuple<string, string, bool> UploadProductPdf(long? productID, string fileDescription, IFormFile file)
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

                //Get the new file path which is under wwwroot\softwarePdf
                // Maybe a option to change the file name...
                newFilePath = Path.Combine(hostingEnv.WebRootPath, "softwarePdf", newFileName);

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


            return new Tuple<string, string, bool>(fileDescription, newFilePath, hasFile);
        }


        public Tuple<string, FileStream, bool> GetSpecificationFilePdf(long productID)
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


        //?????????????????????????????????????????
        public Tuple<string, string, bool> RemoveProductPDF(long productID)
        {
            Tuple<string, string, bool> oldFile = new Tuple<string, string, bool>("", "", false);

            //check if there exist already one old file
            if (productID != 0)
            {
                oldFile = repository.GetSpecificationFilePathOfProduct(productID);

                // string fullPath = Request.MapPath("~/uploaded/" + file);
                if (System.IO.File.Exists(oldFile.Item2))
                {
                    System.IO.File.Delete(oldFile.Item2);
                }
            }

            return oldFile;
        }

        
    }
}
