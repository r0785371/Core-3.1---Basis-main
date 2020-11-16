using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Asset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AssetID { get; set; }

        [Display(Name = "Product")]
        public long PurchaseItemID { get; set; }
        
        [Display(Name = "Product")]
        public PurchaseItem PurchaseItem { get; set; }

        [Display(Name = "Status")]
        public long StatusID { get; set; } //FK is requiered.
        [Display(Name = "Status")]
        public Status Status { get; set; }

        //[Required]
        [StringLength(15)]
        [Display(Name = "Asset tag")]
        public string AssetTag { get; set; }

        [StringLength(30)]
        [Display(Name = "Serial number")]
        public string SerieNo { get; set; }

        [StringLength(30)]
        [Display(Name = "Service tag number")]
        public string ServiceTag { get; set; }

        [StringLength(255)]
        [Display(Name = "Target description")]
        public string TargetDescription { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Expire period (m)")]
        public int ExpirePeriodMonth { get; set; } = 36;

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expire date")]
        public DateTime ExpireDate { get; set; }

        [Display(Name = "Warning period (m)")]
        public long WarningPeriodID { get; set; }
        [Display(Name = "Warning period")]
        public WarningPeriod WarningPeriod { get; set; }

        [Required]
        [Display(Name = "Warenty (m)")]
        public int WarentyIntervalMonth { get; set; } = 24;

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Warning date")]
        public DateTime WarningDate { get; set; }

        [NotMapped]
        [Display(Name = "Warning urgency")]
        public string WarningUrgency { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Using start date")]
        public DateTime? UsingDateStart { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Using end date")]
        public DateTime? UsingDateEnd { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public byte[] QrCode { get; set; }

        [Display(Name = "Owner")]
        public long AssetOwnerID { get; set; }
        [Display(Name = "Owner")]
        public AssetOwner AssetOwner { get; set; }

        [Display(Name = "Location (Operational site)")]
        public long? OperationalSiteLocationID { get; set; }
        [Display(Name = "Location (Operational site)")]
        public OperationalSiteLocation OperationalSiteLocation { get; set; }
        
        //Deze moeten wij denk ik wel houden... checken... 
        [NotMapped]
        [Display(Name = "Detail")]
        public long? AssetDetailID { get; set; }
        [Display(Name = "Details")]
        public List<AssetDetail> AssetDetails { get; set; }

        [NotMapped]
        [Display(Name = "License")]
        public long? AssetLicenseID { get; set; }
        [Display(Name = "Licenses")]
        public List<AssetLicense> AssetLicenses { get; set; }

        [Display(Name = "Rack")]
        public long? RackLocationID { get; set; }

        public RackLocation RackLocation { get; set; }

        [Display(Name = "URack")]
        public long? URackID { get; set; }
        public URack URack { get; set; }

        [Display(Name = "Backup")]
        public long? BackupID { get; set; }
        [Display(Name = "Backup")]
        public List<Backup> Backups { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Max backup date")]
        public DateTime MaxBackupDate { get; set; }

        [NotMapped]
        [Display(Name = "Backup urgency")]
        public string BackupUrgency { get; set; }

        List<AssetHistory> AssetHistories { get; set; }
    }
}
