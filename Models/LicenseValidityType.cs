using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class LicenseValidityType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LicenseValidityTypeID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = "License")]
        public long? LicenseID { get; set; }
        public List<License> Licenses { get; set; }
    }
}
