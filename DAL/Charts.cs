using DAL.interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Charts : ICharts
    {
        readonly DataContext context;

        public Charts(DataContext _context)
        {
            context = _context;
        }

        //public void AssetChart(out string AssetTotal, out string AssetOwner)
        //{          
        //}

        public List<Asset> GetAllAssets()
        {
            return context.Assets.ToList();
        }
    }
}
