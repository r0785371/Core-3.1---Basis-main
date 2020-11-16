using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class ExternCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExternCompanyID { get; set; }

        [Required]
        [Display(Name = "Ref.")]
        public string Ref { get; set; }

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
        public string Tel { get; set; }

        [EmailAddress]
        [Display(Name = "Mail")]
        public string Mail { get; set; }

        [Url]
        [Display(Name = "Webpage")]
        public string WebSite { get; set; }

        [Display(Name = "Active")]
        public bool IsActief { get; set; } = true;

        [Display(Name = "Asset Owner")]
        public AssetOwner AssetOwer { get; set; }
    }
}
