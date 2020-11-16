using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class SoftwareType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SoftwareTypeID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public List<Software> ListSoftware { get; set; }
    }
}
