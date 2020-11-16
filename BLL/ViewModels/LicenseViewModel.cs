using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ViewModels
{
    public class LicenseViewModel
    {

        public License license { get; set; }

        [Display(Name = "File description")]
        public string FileDescription { get; set; }
        public IFormFile File { get; set; }

        public List<Asset> assets { get; set; }

        public LicenseViewModel()
        {
            assets = new List<Asset>();
        }
    }
}
