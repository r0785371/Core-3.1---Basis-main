using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DAL
{
    public class SoftwareRepository: ISoftwareRepository
    {
        readonly DataContext context;

        public SoftwareRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Software> GetAllSoftware()
        {
            return context.Software
                .Include(s => s.ProductType)
                .Include(s => s.SoftwareType)
                .Include(s => s.ProductSuppliers)
                .Include(s => s.Status)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public IEnumerable<Software> GetAllSoftwareEnum()
        {
            return context.Software
                .Include(s => s.ProductType)
                .Include(s => s.SoftwareType)
                .Include(s => s.ProductSuppliers)
                .Include(s => s.Status)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public List<SelectListItem> GetSelectListSoftware()
        {
            return context.Software.Select(s => new SelectListItem
            {
                Value = s.ProductID.ToString(),
                Text = s.Name,
            }).OrderBy(o => o.Text).ToList();
        }

        public List<Software> GetAllSoftwareOfStatus(long statusID)
        {
            return context.Software
                .Where(s => s.StatusID == statusID)
                .Include(s => s.ProductType)
                .Include(s => s.SoftwareType)
                .Include(s => s.ProductSuppliers)
                .Include(s => s.Status)
                .ToList();
        }

        public List<Software> GetAllSoftwareOfSoftwareType(long softwareTypeID)
        {
            return context.Software
                .Where(s => s.SoftwareTypeID == softwareTypeID)
                .Include(s => s.ProductType)
                .Include(s => s.SoftwareType)
                .Include(s => s.ProductSuppliers)
                .Include(s => s.Status)
                .ToList();
        }

        public Software FindById(long id)
        {
            return context.Software
                .Where(s => s.ProductID == id)
                .Include(s => s.ProductType)
                .Include(s => s.SoftwareType)
                .Include(s => s.ProductSuppliers)
                .Include(s => s.Status)
                .Single();
        }

        public List<Software> GetAllSoftwareOfSupplier(long supplierID)
        {
            return context.Software
                .Where(s => s.ProductSuppliers.Any(p => p.SupplierID == supplierID))
                .Include(s => s.ProductType)
                .Include(s => s.SoftwareType)
                .Include(s => s.ProductSuppliers)
                .Include(s => s.Status)
                .ToList();
        }

        public bool SoftwareExists(long id)
        {
            return context.Software.Any(s => s.ProductID == id);
        }

        public Software Add(Software software)
        {
            context.Software.Add(software);
            context.SaveChanges();
            return software;
        }

        public void Update(Software software)
        {

            //As we call the methode GetSpecificationFilePathOfProduct te get the full path of the productPdf
            // https://entityframework.net/knowledge-base/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-type-with-the-same-key-is-already-being-tracked

            // 
            var local = context.Set<Software>()
                .Local
                .FirstOrDefault(entry => entry.ProductID.Equals(software.ProductID));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }

            // set Modified flag in your entry
            context.Entry(software).State = EntityState.Modified;

            // save 
            context.SaveChanges();

            ////Old way
            //context.Software.Update(software);
            //context.SaveChanges();
        }

        public void Remove(long id)
        {
            var software = context.Software.SingleOrDefault(s => s.ProductID == id);
            context.Software.Remove(software);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Tuple<string, string, bool> GetSpecificationFilePathOfProduct(long productID)
        {
            var software = context.Software
                .Where(h => h.ProductID == productID)
                .Single();

            if (string.IsNullOrEmpty(software.SpecificationFileName))
            {
                software.SpecificationFileName = "";
            }

            if (string.IsNullOrEmpty(software.SpecificationFilePath))
            {
                software.SpecificationFilePath = "";
            }

            return new Tuple<string, string, bool>(software.SpecificationFileName, software.SpecificationFilePath, software.HasFile);
        }

        public Tuple<string, string, bool> RemoveSpecificationFilePdf(long productID)
        {
            Tuple<string, string, bool> oldFile = new Tuple<string, string, bool>("", "", false);

            if (productID != 0)
            {
                //Get info of the file from this hardware.
                oldFile = GetSpecificationFilePathOfProduct(productID);

                // check if this Hardware has a PDF and if has one path 
                if (oldFile.Item3 == true && !string.IsNullOrEmpty(oldFile.Item2))
                {
                    // check if other records also uses the same PDF (path).
                    int QtyHardwareWithSamePDF = context.Hardwares
                            .Where(h => h.SpecificationFilePath == oldFile.Item2)
                            .Count();

                    //if no other Hardware uses this PDF...
                    if (QtyHardwareWithSamePDF == 1)
                    {
                        //... checkes if the file exits in the map:
                        // string fullPath = Request.MapPath("~/uploaded/" + file);
                        if (System.IO.File.Exists(oldFile.Item2))
                        {
                            //...if so, remove the file!
                            System.IO.File.Delete(oldFile.Item2);
                        }
                    }

                    return oldFile;
                }
            }
            return oldFile;
        }
    }
}
