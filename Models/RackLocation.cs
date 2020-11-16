using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class RackLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RackLocationID { get; set; }

        [Display(Name = "Location")]
        public long LocationID { get; set; }
        public Location Location { get; set; }

        [Display(Name = "Rack")]
        public long RackID { get; set; }
        public Rack Rack { get; set; }

        [Display(Name = "Rack no.")]
        [Required]
        [StringLength(20)]
        public string RackNo { get; set; }

        public List<Asset> Assets { get; set; }

    }
}
