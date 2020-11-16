using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    // Changes the name in form where dropbox is used
    [Display(Name = "Main detail")]
    public class DetailMain
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DetailMainID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Main detail")]
        public string Name { get; set; }

        [NotMapped]
        public long? DetailID { get; set; }
        public List<Detail> Details { get; set; }
    }
}
