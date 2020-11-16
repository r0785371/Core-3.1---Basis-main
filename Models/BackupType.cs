using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class BackupType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BackupTypeID { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = "Backups")]
        public long? BackupId { get; set; }
        public List<Backup> Backups { get; set; }

    }
}
