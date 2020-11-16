using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ViewModels
{
    public class softwareViewModel
    {
        public Software software { get; set; }

        [Display(Name = "File description")]
        public string FileDescription { get; set; }
        public IFormFile File { get; set; }

        public List<ProductSupplier> productSuppliers { get; set; }
        public List<ProductDetail> productDetails { get; set; }
        public List<PurchaseItem> purchaseItems { get; set; }

        public softwareViewModel()
        {
            productSuppliers = new List<ProductSupplier>();
            productDetails = new List<ProductDetail>();
            purchaseItems = new List<PurchaseItem>();
        }
    }
}
