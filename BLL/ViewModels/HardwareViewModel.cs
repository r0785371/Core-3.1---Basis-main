using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ViewModels
{
    public class HardwareViewModel
    {
        public Hardware hardware { get; set; }

        [Display(Name = "File description")]
        public string FileDescription { get; set; }
        public IFormFile File { get; set; }


        public List<ProductSupplier> productSuppliers { get; set; }
        public List<ProductDetail> productDetails { get; set; }
        public List<PurchaseItem> purchaseItems { get; set; }

        public HardwareViewModel()
        {
            productSuppliers = new List<ProductSupplier>();
            productDetails = new List<ProductDetail>();
            purchaseItems = new List<PurchaseItem>();
        }
    }
}
