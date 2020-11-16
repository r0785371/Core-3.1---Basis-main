using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StatusID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Status")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Display(Name = "Product")]
        public bool HasProduct { get; set; }
        [Display(Name = "Seq. product")]
        public int? ProductSequence { get; set; }
        [NotMapped]
        [Display(Name = "Product")]
        public long? ProductID { get; set; }
        public List<Product> Products { get; set; }

        [Display(Name = "No support")]
        public bool NoSupport { get; set; }

        [Display(Name = "Purchase")]
        public bool HasPurchase { get; set; }
        [Display(Name = "Seq. purchase")]
        public int? PurchaseSequence { get; set; }

        [NotMapped]
        [Display(Name = "Purchase")]
        public long? PurchaseItemID { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }

        [Display(Name = "Asset")]
        public bool HasAsset { get; set; }
        [Display(Name = "Seq. asset")]
        public int? AssetSequence { get; set; }
        [NotMapped]
        [Display(Name = "Asset")]
        public long? AssetID { get; set; }
        public List<Asset> Assets { get; set; }

        [Display(Name = "License")]
        public bool HasLicense { get; set; }
        [Display(Name = "Seq. license")]
        public int? LicenceSequence { get; set; }
        [NotMapped]
        [Display(Name = "License")]
        public long? LicenseID { get; set; }
        public List<License> Licenses { get; set; }

        [Display(Name = "Create Asset/License")]
        public bool GenerateAssetOrLicense { get; set; }

        [Display(Name = "To order)")]
        public bool ToOrder { get; set; }

        [Display(Name = "Ordered)")]
        public bool Ordered { get; set; }

        [Display(Name = "On stock)")]
        public bool OnStock { get; set; }

        [Display(Name = "In Use (BackUp)")]
        public bool InUse { get; set; }

        [Display(Name = "Out of Use")]
        public bool OutOfUse { get; set; }

        public List<AssetHistory>  AssetHistories { get; set; }
    }
}
