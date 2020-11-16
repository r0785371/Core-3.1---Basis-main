using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    // Will change the name at dropboxe list
    [Display(Name ="Sub detail")]
    public class DetailSub
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DetailSubID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Sub detail")]
        public string Name { get; set; }

        [NotMapped]
        public long? DetailID { get; set; }
        public List<Detail> Details { get; set; }
    }
}
