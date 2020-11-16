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
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class SoftwareController : Controller
    {
        private readonly ISoftwareService service;

        public SoftwareController(ISoftwareService _service)
        {
            service = _service;
        }

        // GET: Software
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["ProductTypeSortParm"] = sortOrder == "ProductType" ? "productType_desc" : "ProductType";
            ViewData["SoftwareTypeSortParm"] = sortOrder == "SoftwareType" ? "softwareType_desc" : "SoftwareType";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "description_desc" : "Description";
            
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Software> softwares = service.GetAllSoftwareEnum(searchString);

            switch (sortOrder)
            {
                case "name_desc":
                    softwares = softwares.OrderByDescending(h => h.Name != null ? h.Name : "").ThenByDescending(h => h.Status.Name != null ? h.Status.Name : "");
                    break;
                case "Status":
                    softwares = softwares.OrderBy(s => s.Status.Name);
                    break;
                case "status_desc":
                    softwares = softwares.OrderByDescending(s => s.Status.Name);
                    break;
                case "ProductType":
                    softwares = softwares.OrderBy(s => s.ProductType.Name);
                    break;
                case "productType_desc":
                    softwares = softwares.OrderByDescending(s => s.ProductType.Name);
                    break;
                case "SoftwareType":
                    softwares = softwares.OrderBy(h => h.SoftwareType.Name);
                    break;
                case "softwareType_desc":
                    softwares = softwares.OrderByDescending(h => h.SoftwareType.Name);
                    break;
                case "Description":
                    softwares = softwares.OrderBy(h => h.Description);
                    break;
                case "description_desc":
                    softwares = softwares.OrderByDescending(h => h.Description);
                    break;
                
                default:
                    softwares = softwares.OrderBy(h => h.Name != null ? h.Name : "").ThenBy(h => h.Status.Name != null ? h.Status.Name : "");
                    break;
            }


            //List<Software> softwares = service.GetAllSoftware();

            return View(softwares);
        }

        // GET: Software/Details/
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Software software = service.FindById(id.Value);
            if (software == null)
            {
                return NotFound();
            }

            return View(software);
        }

        // GET: Software/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeSoftware());
            ViewData["SoftwareTypeID"] = new List<SelectListItem>(service.GetSelectListSoftwareTypes());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusProduct());

            return View();
        }

        // POST: Software/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create(softwareViewModel softwareViewModel, [Bind("ProductID,StatusID,SoftwareTypeID,SoftwareVersion,ProductTypeID,ProductSupplierID,Name,Description,InfoUrl,Image,SpecificationFile")] Software software)
        {

            //upload file
            Tuple<string, string, bool> filePath = service.UploadProductPdf(softwareViewModel.software.ProductID, softwareViewModel.FileDescription, softwareViewModel.File);

            //add FileName and FilePath to hardware so that this will be saved to the database
            software.SpecificationFileName = filePath.Item1;
            software.SpecificationFilePath = filePath.Item2;
            software.HasFile = filePath.Item3;

            if (ModelState.IsValid)
            {
                software = service.Add(software);

                //Return to Edit view so that customer can see the software just added + can add supplier(s) info.
                return RedirectToAction("Edit", new { id = software.ProductID });
            }

            return View(softwareViewModel);
        }

        // GET: Software/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
                //return RedirectToAction("Index");
            }

            softwareViewModel software = service.LoadSoftwareViewModel(id.Value);

            if (software == null)
            {
                return NotFound();
            }

            ViewData["ProductTypeID"] = new List<SelectListItem>(service.GetSelectListProductTypeSoftware());
            ViewData["SoftwareTypeID"] = new List<SelectListItem>(service.GetSelectListSoftwareTypes());
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusProduct());

            return View(software);
        }

        // POST: Software/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, softwareViewModel softwareViewModel, string productChild, bool? goToProductSupplier, bool? goToProductDetail, [Bind("ProductID,StatusID,SoftwareTypeID,SoftwareVersion,ProductTypeID,ProductSupplierID,Name,Description,InfoUrl,Image,SpecificationFile")] Software software)
        {
            if (id != software.ProductID)
            {
                return NotFound();
            }

            Tuple<string, string, bool> filePath = service.UploadProductPdf(softwareViewModel.software.ProductID, softwareViewModel.FileDescription, softwareViewModel.File);

            //add FileName and FilePath to software so that this will be saved to the database
            software.SpecificationFileName = filePath.Item1;
            software.SpecificationFilePath = filePath.Item2;
            software.HasFile = filePath.Item3;

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(software);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareExists(software.ProductID))
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
                    return RedirectToAction("Create", "ProductSupplier", new { productID = software.ProductID, productChild });
                }
                else if (goToProductDetail == true)
                {
                    return RedirectToAction("Create", "ProductDetail", new { productID = software.ProductID, productChild });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewData["ProductTypeID"] = new SelectList(service.GetSelectListProductTypeSoftware(), software.ProductTypeID);
            ViewData["SoftwareTypeID"] = new SelectList(service.GetSelectListSoftwareTypes(), software.SoftwareTypeID);
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusProduct());

            return View(software);
        }

        // GET: Software/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tuple<long, Software, List<ProductSupplier>, List<ProductDetail>, List<PurchaseItem>> software = service.GetAllSoftwareOfProduct(id.Value);

            if (software.Item2 == null)
            {
                return RedirectToAction("Edit", new { id });
            }

            ViewData["ListProductSupplier"] = software.Item3;
            ViewData["ListProductDetail"] = software.Item4;
            ViewData["ListPurchaseItem"] = software.Item5;

            return View(software.Item2);
        }

        // POST: Software/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SoftwareExists(long id)
        {
            return service.SoftwareExists(id);
        }



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
        }

        #endregion


        #region RemoveSpecificationFilePdf

        // POST: Software/RemoveSpecificationFilePdf/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult RemoveSpecificationFilePdf(long id, softwareViewModel softwareViewModel, [Bind("ProductID,StatusID,SoftwareTypeID,SoftwareVersion,ProductTypeID,ProductSupplierID,Name,Description,InfoUrl,Image,SpecificationFile")] Software software)
        {
            if (id != software.ProductID)
            {
                return NotFound();
            }

            //Tuple<string, string, bool> filePath = service.UploadProductPdf(softwareViewModel.software.ProductID, softwareViewModel.FileDescription, softwareViewModel.File);
            Tuple<string, string, bool> removePDF = service.RemoveSpecificationFilePdf(softwareViewModel.software.ProductID);

            software.SpecificationFileName = "";
            software.SpecificationFilePath = "";
            software.HasFile = false;

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(software);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftwareExists(software.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Edit", "Software", new { id = software.ProductID });

            }
            ViewData["ProductTypeID"] = new SelectList(service.GetSelectListProductTypeSoftware(), software.ProductTypeID);
            ViewData["SoftwareTypeID"] = new SelectList(service.GetSelectListSoftwareTypes(), software.SoftwareTypeID);
            ViewData["StatusID"] = new List<SelectListItem>(service.GetSelectListStatusProduct());

            return View(software);
        }

        #endregion
    }
}
