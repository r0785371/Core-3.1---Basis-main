using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels
{
    public class OperationalSiteViewModel
    {
        public OperationalSite operationalSite { get; set; }

        public AssetOwner assetOwner { get; set; }
        public List<Asset> assets { get; set; }
        public List<OperationalSiteLocation> operationalSiteLocations { get; set; }
        public List<Location> locations { get; set; }

        public OperationalSiteViewModel()
        {
            assetOwner = new AssetOwner();
            assets = new List<Asset>();
            operationalSiteLocations = new List<OperationalSiteLocation>();
            locations = new List<Location>();
        }



    }
}
