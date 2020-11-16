using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class AssetHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AssetHistoryID { get; set; }

        [Required]
        [Display(Name = "Asset")]
        public long AssetID { get; set; }
        public Asset  Asset { get; set; }

        [Required]
        [Display(Name = "Status")]
        public long StatusID { get; set; }

        public Status Status { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        [MaxLength(256)]
        [Display(Name ="User")]
        public string NameUser { get; set; }

        //[NotMapped]
        //public UserManager<ApplicationUser> Users { get; set; }

        public string Description { get; set; }
    }
}
