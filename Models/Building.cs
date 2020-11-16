using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Building
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BuildingID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Reference")]
        public string Ref { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(10)]
        public string Number { get; set; }

        [Display(Name = "Zip code")]
        [StringLength(10)]
        public string Postcode { get; set; }

        [Display(Name = "City")]
        [StringLength(30)]
        public string City { get; set; }

        ////[NotMapped]
        //[Display(Name = "GeoLocation")]
        //[Column(TypeName ="Geometry")]
        
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
        //public string Accuracy { get; set; }
        
        [Display(Name = "Locations")]
        public List<Location> Locations { get; set; }

    }
}
