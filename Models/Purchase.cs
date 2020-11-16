using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Purchase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseID { get; set; }

        [Display(Name ="Type")]
        public long PurchaseTypeID { get; set; }
        [Display(Name = "Type")]
        public PurchaseType PurchaseType { get; set; }
        [NotMapped]
        public List<PurchaseType> ListPurchaseTypes { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public long SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Number")]
        public string No { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order date")]
        public DateTime Date { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; }

    }

}
