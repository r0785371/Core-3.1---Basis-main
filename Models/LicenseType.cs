using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class LicenseType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LicenseTypeID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = "License")]
        public long? LicenceID { get; set; }
        public List<License> Licenses { get; set; }

        [Display(Name = "Limited use")]
        public bool LimitedUse { get; set; }

        [Display(Name = "Unlimited use")]
        public bool UnlimitedUse { get; set; }
    }
}
