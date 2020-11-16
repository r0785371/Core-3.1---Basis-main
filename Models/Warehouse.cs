using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
     public class Warehouse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long  WarehouseID { get; set; }

        [Required]
        [StringLength(10)]
        public string Ref { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //Check if this is correct that 1x Warehouse can have only 1x location!!!???
        //Otherwise it's a many to many relationship!
        //Warehouse heeft maar 1 locatie
        [Display(Name = "Location")]
        public long LocationId { get; set; }
        public Location Location { get; set; }

        [Display(Name = "Asset Owner")]
        public AssetOwner AssetOwer { get; set; }

    }
}
