using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class ProductSupplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductSupplierID { get; set; }

        [Display(Name = "Supplier")]
        public long SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        [Display(Name = "Product")]
        public long ProductID { get; set; }
        public Product Product { get; set; }

        [StringLength(30)]
        [Display(Name = "Ref. of supplier")]
        public string RefSupplier { get; set; }

        [StringLength(50)]
        [Display(Name = "Product name of supplier")]
        public string ProductNameSupplier { get; set; }

        //Price double or Decimal... (decimal should be enough, example was decimal!!)

        //[DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:#.####,00}")]
        public double Price { get; set; }
    }
}
