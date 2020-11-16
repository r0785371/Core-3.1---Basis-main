
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Models
{
    
   public class Floor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FloorID { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Reference")]
        public string Ref { get; set; }

        [Display(Name = "Name")]
        [StringLength(25)]
        public string Name { get; set; }

        public int Sequence { get; set; }

        [Display(Name = "Location")]
        public List<Location> Locations { get; set; }
    }
}
