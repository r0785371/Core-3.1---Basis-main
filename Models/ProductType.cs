using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProductType
    {
        public ProductType()
        {
            ListProductTypeGroup = new HashSet<ProductType>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductTypeID { get; set; }

        [Display(Name = "Type")]
        public ProductChildren ProductChild { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Ref.")]
        public string Ref { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Display(Name = "Is group")]
        public bool IsGroup { get; set; }

        //virtual????
        [Display(Name = "Group")]
        public long? ProductTypeGroupID { get; set; } = null;
        [Display(Name = "Group")]
        public virtual ProductType ProductTypeGroup { get; set; }
        public virtual ICollection<ProductType> ListProductTypeGroup { get; set; }

        [Display(Name = "Is hardware")]
        public bool IsProduct { get; set; }

        [Display(Name = "Is software / license")]
        public bool IsLicense { get; set; }

        [Display(Name = "Is asset")]
        public bool IsAsset { get; set; }

        [Required]
        [Display(Name = "Standard expire time (in month)")]
        public int ExpirePeriodMonth { get; set; } = 36;

        [Display(Name = "Standard warning period (in month)")]
        public long? WarningPeriodID { get; set; }
        public WarningPeriod WarningPeriod { get; set; }
        //[NotMapped]
        //public List<WarningPeriod> WarningPeriods { get; set; }

        [Display(Name = "Tag number")]
        public string TagNo { get; set; }


        [Display(Name = "Is component")]
        public bool IsComponent { get; set; }

        [Display(Name = "Has backup")]
        public bool HasBackup { get; set; }

        [Required]
        [Display(Name = "Backup interval (months)")]
        public int BackupInterval { get; set; } = 0;

        [Display(Name = "Go in rack")]
        public bool GoInRack { get; set; }

        [Display(Name = "Products")]
        public List<Product> Products { get; set; }

    }

    public enum ProductChildren { Hardware = 0, Software = 1 };
}
