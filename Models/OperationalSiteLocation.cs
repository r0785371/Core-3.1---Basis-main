using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class OperationalSiteLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Display(Name = "Operation site location")]
        public long OperationalSiteLocationID { get; set; }

        [Required]
        [Display(Name = "Operational site")]
        public long OperationalSiteID { get; set; }
        [Display(Name = "Operational site")]
        public OperationalSite OperationalSite { get; set; }

        [Required]
        [Display(Name = "Location")]
        public long LocationID { get; set; }
        public Location Location { get; set; }

        public List<Asset> Assets { get; set; }
    }
}
