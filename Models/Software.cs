 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Software: Product
    {
        [Display(Name = "Software")]
        public long SoftwareTypeID { get; set; }
        [Display(Name = "Software")]
        public SoftwareType SoftwareType { get; set; }

        [Display(Name = "Version")]
        public string SoftwareVersion { get; set; }
    }
}
