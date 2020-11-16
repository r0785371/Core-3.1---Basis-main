using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class License
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LicenseID { get; set; }

        [Required]
        [Display(Name = "Status")]
        public long StatusID { get; set; }
        public Status Status { get; set; }

        [Display(Name ="Product")]
        public long PurchaseItemID { get; set; }
        [Display(Name = "Product")]
        public PurchaseItem PurchaseItem { get; set; }

        [Display(Name = "Number")]
        public string No { get; set; }

        public string Key { get; set; }
        
        [Display(Name = "COL")]
        public bool HasCol { get; set; }

        [Display(Name = "Certificate name")]
        public string ColFileName { get; set; }

        public string ColFilePath { get; set; }

        [Display(Name = "Validity in months")]
        public int ValidityTypeTime { get; set; }

        [Display(Name = "Type")]
        public long LicenseTypeID { get; set; }
        [Display(Name = "Type")]
        public LicenseType LicenseType { get; set; }

        [Display(Name = "Validity type")]
        public long LicenseValidityTypeID { get; set; }
        [Display(Name = "Validity type")]
        public LicenseValidityType LicenseValidityType { get; set; }

        public bool AddToAsset { get; set; }

        [Display(Name = "Asset")]
        public long? AssetLicenseID { get; set; }
        public List<AssetLicense> AssetLicenses { get; set; }

        [Display(Name = "Qty")]
        public int QtyLimited { get; set; } = 1;

        [Display(Name = "Parent")]
        public long? ParentLicenseID { get; set; }
        [Display(Name = "Parent")]
        public List<License> Licenses { get; set; }
    }
}
