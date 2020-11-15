using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SupplierID { get; set; }

        [Required]
        [Display(Name = "Company")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(10)]
        public string Number { get; set; }

        [StringLength(12)]
        [Display(Name = "Zip code")]
        public string Postcode { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(30)]
        public string State { get; set; }

        [StringLength(30)]
        public string Country { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string ServiceDeskTel { get; set; }

        [EmailAddress]
        [Display(Name = "Mail")]
        public string ServiceDeskMail { get; set; }

        [Url]
        [Display(Name = "Webpage")]
        public string ServiceDeskWeb { get; set; }

        [Display(Name = "Active")]
        public bool IsActief { get; set; } = true;

        [Display(Name = "Hardware")]
        public bool HasHardware { get; set; }

        [Display(Name = "Software")]
        public bool HasSoftware { get; set; }

        //public List<ProductSupplier> ProductSuppliers { get; set; }

        //public List<Purchase> Purchases { get; set; }
    }
}
