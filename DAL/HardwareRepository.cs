using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public class HardwareRepository: IHardwareRepository
    {
        readonly DataContext context;

        public HardwareRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Hardware> GetAllHardware()
        {
            return context.Hardwares
                .Include(s => s.Status)
                .Include(s => s.ProductType)
                .Include(s => s.ProductSuppliers)
                .ToList();
        }

        public IEnumerable<Hardware> GetAllHardwareEnum()
        {
            return context.Hardwares
                .Include(s => s.Status)
                .Include(s => s.ProductType)
                .Include(s => s.ProductSuppliers)
                .ToList();
        }

        public IQueryable<Hardware> GetAllHardwareIQuery()
        {
            return context.Hardwares
                .Include(s => s.Status)
                .Include(s => s.ProductType)
                .Include(s => s.ProductSuppliers);
        }
        

        public List<SelectListItem> GetSelectListHardware()
        {
            return context.Hardwares.Select(s => new SelectListItem
            {
                Value = s.ProductID.ToString(),
                Text = s.Name,
                //Selected=c.ProductID.Equals(1)
            }).ToList();
        }
        
        public List<Hardware> GetAllHardwareOfStatus(long statusID)
        {
            return context.Hardwares
                .Where(h => h.StatusID == statusID)
                .Include(s => s.Status)
                .Include(s => s.ProductType)
                .Include(s => s.ProductSuppliers)
                .ToList();
        }

        public List<Hardware> GetAllHardwareOFSupplier(long supplierID)
        {
            return context.Hardwares
                .Where(h => h.ProductSuppliers.Any(s => s.SupplierID == supplierID))
                .Include(s => s.Status)
                .Include(s => s.ProductType)
                .Include(s => s.ProductSuppliers)
                .ToList();

        }

        public Hardware FindById(long id)
        {
            return context.Hardwares
                .Include(s => s.Status)
                .Include(s => s.ProductType)
                .Include(s => s.ProductSuppliers)
                .Where(s => s.ProductID == id).Single();
        }

        public bool HardwareExists(long id)
        {
            return context.Hardwares.Any(s => s.ProductID == id);
        }

        public Hardware Add(Hardware hardware)
        {
            context.Hardwares.Add(hardware);
            context.SaveChanges();
            return hardware;
        }

        public void Update(Hardware hardware)
        {

            //As we call the methode GetSpecificationFilePathOfProduct te get the full path of the productPdf
            // https://entityframework.net/knowledge-base/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-type-with-the-same-key-is-already-being-tracked

            // 
            var local = context.Set<Hardware>()
                .Local
                .FirstOrDefault(entry => entry.ProductID.Equals(hardware.ProductID));

            // check if local is not null 
            if (local != null)
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }

            // set Modified flag in your entry
            context.Entry(hardware).State = EntityState.Modified;

            // save 
            context.SaveChanges();

            ////Old way
            //context.Hardwares.Update(hardware);
            //context.SaveChanges();
        }

        public void Remove(long id)
        {
            var hardware = context.Hardwares.SingleOrDefault(s => s.ProductID == id);
            context.Hardwares.Remove(hardware);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Tuple<string, string, bool> GetSpecificationFilePathOfProduct(long productID)
        {
            var hardware = context.Hardwares
                .Where(h => h.ProductID == productID)
                .Single();

            if (string.IsNullOrEmpty(hardware.SpecificationFileName ))
            {
                hardware.SpecificationFileName = "";
            }

            if (string.IsNullOrEmpty(hardware.SpecificationFilePath))
            {
                hardware.SpecificationFilePath = "";
            }

            return new Tuple<string, string, bool>(hardware.SpecificationFileName, hardware.SpecificationFilePath, hardware.HasFile);
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
