using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Rack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Rack")]
        public long RackID { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Rack location")]
        public List<RackLocation> RackLocations { get; set; }
    }
}
