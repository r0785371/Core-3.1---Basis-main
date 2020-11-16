using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
     public class Statistics
    {





    }

    public class ListAssetsPivotData
    {
        //public long ID { get; set; }

        [Display(Name ="Product")]
        public string ProductName { get; set; }

        [Display(Name = "Delivery month")]
        public string DeliveryDateMonth { get; set; }

        [Display(Name = "Delivery year")]
        public string DeliveryDateYear { get; set; }

        [Display(Name = "Expire month")]
        public string ExpireDateMonth { get; set; }

        [Display(Name = "Expire year")]
        public string ExpireDateYear { get; set; }

        public string Owner { get; set; }

        [Display(Name = "Owner type")]
        public string OwnerType { get; set; }

        //public bool OwnerExternCompany { get; set; }

        //public bool OwnerGroupPeople { get; set; }

        //public bool OwnerOperationalSite { get; set; }

        //public bool OwnerPeople { get; set; }

        //public bool OwnerWarehouse { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Product type")]
        public string ProductType { get; set; }

        [Display(Name = "Warning period month")]
        public string WarningPeriodMonth { get; set; }

        [Display(Name = "Warning period year")]
        public string WarningPeriodYear { get; set; }

        [Display(Name = "Warning period urgency")]
        public string WarningUrgency { get; set; }

        public string Location { get; set; }

        public string Rack { get; set; }

        public string URack { get; set; }
    }


    public class OperationalSiteStatistics
    {

        public int Computer { get; set; }

        public int EngStation { get; set; }
        public int ScadaServer { get; set; }
        public int Switch { get; set; }
        public int Drive { get; set; }
        public int Ups { get; set; }
        public int PLC { get; set; }
        public int Laptop { get; set; }
        public int License { get; set; }

        internal OperationalSiteStatistics Accumulate(ProductType productType)
        {
            switch (productType.ProductTypeID)
            {
                case 1:
                    Computer++;
                    break;
                case 2:
                    Switch++;
                    break;
                case 3:
                    Drive++;
                    break;
                case 4:
                    Ups++;
                    break;
                case 5:
                    PLC++;
                    break;
                case 7:
                    EngStation++;
                    break;
                case 8:
                    ScadaServer++;
                    break;
                case 10:
                    Laptop++;
                    break;
                    //default:
                    //    break;
            }
            return this;
        }
        public OperationalSiteStatistics Compute()
        {
            return this;
        }
    }

}
