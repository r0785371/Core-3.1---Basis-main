using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class PurchaseItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseItemID { get; set; }

        [Display(Name = "Purchase")]
        public long PurchaseID { get; set; }
        public Purchase Purchase { get; set; }

        [Display(Name = "Product")]
        public long ProductID { get; set; }
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int PurchaseQty { get; set; }

        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Warenty in month")]
        public int WarentyIntervalMonth { get; set; } = 24;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Status")]
        public long StatusID { get; set; } //FK is requiered.
        [Display(Name = "Status")]
        public Status Status { get; set; }

        public bool HasAssetOrLicense { get; set; }

        //Should be not mapped!?
        public long? AssetID { get; set; }
        public List<Asset> Assets { get; set; }

        //Should be not mapped!?
        public long? LicenseID { get; set; }
        public List<License> Licenses { get; set; }
    }
}
