using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ICharts
    {
        List<Asset> GetAllAssets();
        // void AssetChart(out string AssetTotal, out string assetOwner);


    }
}
