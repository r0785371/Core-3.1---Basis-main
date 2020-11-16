using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    //Product is the parent of Hardware and Software
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductID { get; set; }

        [Display(Name ="Type")]
        public long ProductTypeID { get; set; }
        [Display(Name = "Type")]
        public ProductType ProductType { get; set; }
        
        [Required]
        [StringLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Url]
        [StringLength(255)]
        [Display(Name = "Info URL")]
        public string InfoUrl { get; set; }

        [Display(Name = "Has file")]
        public bool HasFile { get; set; }
        [StringLength(30)]
        [Display(Name = "Spec. file")]
        public string SpecificationFileName { get; set; }
        public string SpecificationFilePath { get; set; }

        [Required]
        [Display(Name = "Status")]
        public long StatusID { get; set; }
        public Status Status { get; set; }

        [NotMapped]
        [Display(Name = "Suppliers + info")]
        public long? ProductSupplierID { get; set; }    //FK kan be null.
        public List<ProductSupplier> ProductSuppliers { get; set; }

        [NotMapped]
        [Display(Name = "Details")]
        public long? ProductDetailID { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
                     
        [NotMapped]
        [Display(Name = "Purchase item")]
        public long? PurchaseItemID { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
    }
}
