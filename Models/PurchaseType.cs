using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class PurchaseType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long PurchaseTypeID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Type")]
        public string Name { get; set; }

        public List<Purchase> Purchases { get; set; }
    }
}
