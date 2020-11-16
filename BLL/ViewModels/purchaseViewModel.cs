using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels
{
    public class purchaseViewModel
    {
        public Purchase purchase { get; set; }

        public List<PurchaseItem> purchaseItems { get; set; }

        public long SelectedPurchaseTypeID { get; set; }

        public purchaseViewModel()
        {
            purchaseItems = new List<PurchaseItem>();
        }

    }
}
