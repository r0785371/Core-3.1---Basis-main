using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;
using BLL.interfaces;
using BLL.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class HardwareController : Controller
    {
        private readonly IHardwareService service;

        public HardwareController(IHardwareService _service)
        {
            service = _service;
        }

        // GET: Hardware
        public IActionResult Index(string sortOrder, string searchString)
            //public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            //ViewData["CurrentSort"] = sortOrder;
            ViewData["NameTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["ProductTypeSortParm"] = sortOrder == "ProductType" ? "productType_desc" : "ProductType";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "description_desc" : "Description";
            ViewData["SpecificationFileNameSortParm"] = sortOrder == "SpecificationFileName" ? "specificationFileName_desc" : "SpecificationFileName";

            //if (searchString != null)
            //{
            //    pageNumber = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            ViewData["CurrentFilter"] = searchString;

            //var hardwares = service.GetAllHardwareIQuery(searchString);

            var hardwares = service.GetAllHardwareEnum(searchString);

            switch (sortOrder)
            {
                case "name_desc":
                    hardwares = hardwares.OrderByDescending(h => h.Name ?? "").ThenByDescending(h => h.Status.Name ?? "");
                    break;
                case "Status":
                    hardwares = hardwares.OrderBy(s => s.Status.Name);
                    break;
                case "status_desc":
                    hardwares = hardwares.OrderByDescending(s => s.Status.Name);
                    break;
                case "ProductType":
                    hardwares = hardwares.OrderBy(s => s.ProductType.Name);
                    break;
                case "productType_desc":
                    hardwares = hardwares.OrderByDescending(s => s.ProductType.Name);
                    break;
                case "Description":
                    hardwares = hardwares.OrderBy(h => h.Description);
                    break;
                case "description_desc":
                    hardwares = hardwares.OrderByDescending(h => h.Description);
                    break;
                case "SpecificationFileName":
                    hardwares = hardwares.OrderBy(h => h.SpecificationFileName ?? "");
                    break;
                case "specificationFileName_desc":
                    hardwares = hardwares.OrderByDescending(h => h.SpecificationFileName ?? "");
                    break;
                default:
                    hardwares = hardwares.OrderBy(h => h.Name ?? "").ThenBy(h => h.Status.Name ?? "");
                    break;
            }

            //int pageSize = 3;
            //return View(PaginatedList<Hardware>.CreateAsync(hardwares.AsNoTracking(), pageNumber ?? 1, pageSize));

            //List<Hardware> hardware = service.GetAllHardware();
            return View(hardwares);
        }

        // GET: Hardware/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hardware hardware = service.FindById(id.Value);
            if (hardware == null)
            {
                return NotFound();
            }

            return View(hardware);
        }

        // GET: Hardware/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ProductTypeDropDown();
            StatusDropDown();

            //ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeHardware());
            //ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeHardware());

            return View();
        }

        // POST: Hardware/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(HardwareViewModel hardwareViewModel, [Bind("ProductID,ProductTypeID,Name,Description,StatusID,InfoUrl,SpecificationFile")] Hardware hardware)
        {

            //upload file
            Tuple<string, string, bool> filePath = service.UploadProductPdf(hardwareViewModel.hardware.ProductID, hardwareViewModel.FileDescription, hardwareViewModel.File);

            //add FileName and FilePath to hardware so that this will be saved to the database
            hardware.SpecificationFileName = filePath.Item1;
            hardware.SpecificationFilePath = filePath.Item2;
            hardware.HasFile = filePath.Item3;

            if (ModelState.IsValid)
            {
                hardware = service.Add(hardware);

                return RedirectToAction("Edit", "Hardware", new { id = hardware.ProductID });
                //return RedirectToAction(nameof(Index));
            }

            ProductTypeDropDown(hardware.ProductTypeID);
            StatusDropDown(hardware.StatusID);

            //ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeHardware());
            return View(hardwareViewModel);
        }

        // GET: Hardware/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id, long? assetID)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            HardwareViewModel hardware = service.loadHardwareViewModel(id.Value);

            if (hardware == null)
            {
                return NotFound();
            }

            ProductTypeDropDown(hardware.hardware.ProductTypeID);
            StatusDropDown(hardware.hardware.StatusID);
            //ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeHardware());

            ViewData["assetID"] = assetID;

            return View(hardware);
        }

        // POST: Hardware/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, HardwareViewModel hardwareViewModel, string productChild, bool? goToProductSupplier, bool? goToProductDetail, [Bind("ProductID,ProductTypeID,Name,Description,StatusID,InfoUrl,SpecificationFile")] Hardware hardware)
        {
            if (id != hardware.ProductID)
            {
                return NotFound();
            }

            //if (hardwareViewModel.File != null)
            //{
                //upload file
                Tuple<string, string, bool> filePath = service.UploadProductPdf(hardwareViewModel.hardware.ProductID, hardwareViewModel.FileDescription, hardwareViewModel.File);

                //add FileName and FilePath to hardware so that this will be saved to the database
                hardware.SpecificationFileName = filePath.Item1;
                hardware.SpecificationFilePath = filePath.Item2;
                hardware.HasFile = filePath.Item3;
            //}

            ModelState.Clear();
            TryValidateModel(hardware);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(hardware);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HardwareExists(hardware.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (goToProductSupplier == true)
                {
                    return RedirectToAction("Create", "ProductSupplier", new { productID = hardware.ProductID, productChild });
                }
                else if (goToProductDetail == true)
                {
                    return RedirectToAction("Create", "ProductDetail", new { productID = hardware.ProductID, productChild });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ProductTypeDropDown(hardware.ProductTypeID);
            StatusDropDown(hardware.StatusID);
            //ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeHardware());
            return View(hardware);
        }

        // GET: Hardware/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tuple<long, Hardware, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>> hardware = service.GetAllHardwareOfProduct(id.Value);

            if (hardware.Item2 == null)
            {
                return RedirectToAction("Edit", new { id });
            }

            ViewData["ListProductSupplier"] = hardware.Item3;
            ViewData["ListProductDetail"] = hardware.Item4;
            ViewData["ListPurchaseItem"] = hardware.Item5;

            return View(hardware.Item2);
        }

        // POST: Hardware/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool HardwareExists(long id)
        {
            return service.HardwareExists(id);
        }

        #region DropDown definition

        private void ProductTypeDropDown(object selctedProductType = null)
        {
            ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeHardware());
        }

        private void StatusDropDown(object selctedStatus = null)
        {
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusProduct());
        }

        #endregion


        #region DownloadFilePDF

        public IActionResult DownloadFile(long productID)
        {
            // Will send the productID to service & repository, where it will get the path where the file is
            // from the database. In the service will get the pdf file and convert to FileStream and send back.

            Tuple<string, FileStream, bool> stream = service.GetSpecificationFilePdf(productID);

            if (stream.Item3 == false || stream.Item2 == null)
            {
                //return NotFound();

                return new NotFoundResult();
            }

            // Will send the PDF stream to the view, changing the name!!! (security reasons)
            // In this way no path is send to or from the view!
            return new FileStreamResult(stream.Item2, "application/pdf");


            // FileContentResult - use it when you have a byte array you would like to return as a file
            // FileStreamResult - you have a stream open, you want to return it's content as a file

            //// example how to send the PDF file so that user can save it! 
            //var filePath = "D:\\PortOfAntwerpAppAssets\\PortOfAntwerpAppAssets\\wwwroot\\hardwarePdf\\Port of Antwerp - ERD.pdf";
            //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            //string fileName = "myfile.pdf";
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        #endregion

        #region RemoveSpecificationFilePdf

        // POST: Hardware/RemoveSpecificationFilePdf/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult RemoveSpecificationFilePdf(long id, HardwareViewModel hardwareViewModel, [Bind("ProductID,ProductTypeID,Name,Description,StatusID,InfoUrl,SpecificationFile")] Hardware hardware)
        {
            if (id != hardware.ProductID)
            {
                return NotFound();
            }

            Tuple<string, string, bool> removePDF = service.RemoveSpecificationFilePdf(hardwareViewModel.hardware.ProductID);

            hardware.SpecificationFileName = "";
            hardware.SpecificationFilePath = "";
            hardware.HasFile = false;

            ModelState.Clear();
            TryValidateModel(hardware);

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(hardware);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HardwareExists(hardware.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("Edit", "Hardware", new { id = hardware.ProductID });
            }

            return RedirectToAction("Edit", "Hardware", new { id = hardware.ProductID });
        }

        #endregion
        
    }
}
