using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoomID { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Reference")]
        public string Ref { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Display(Name = "Loacation")]
        public List<Location> Locations { get; set; }
    }
}
