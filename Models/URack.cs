using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class URack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long URackID { get; set; }
                
        [Display(Name = "Number")]
        [StringLength(25)]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = "Assets")]
        public long? AssetId { get; set; }
        public List<Asset> Assets { get; set; }
    }
}
