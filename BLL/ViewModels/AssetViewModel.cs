
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels
{
    public class AssetViewModel
    {
        public Asset asset { get; set; }

        public List<AssetDetail> assetDetails { get; set; }
        public List<Backup> backups { get; set; }
        public List<AssetLicense> assetLicenses { get; set; }

        public List<AssetHistory> assetHistories { get; set; }


        public AssetViewModel()
        {
            assetDetails = new List<AssetDetail>();
            backups = new List<Backup>();
            assetLicenses = new List<AssetLicense>();
            assetHistories = new List<AssetHistory>();
        }


    }
}
