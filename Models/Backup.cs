using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Backup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BackupID { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Backup Date")]
        public DateTime BackupDate { get; set; }

        [ForeignKey("FkPersonId")]
        [Display(Name = "Person")]
        public long PersonId { get; set; }
        public Person Person { get; set; }

        [ForeignKey("FkBackupTypeId")]
        [Display(Name = "Backup Type")]
        public long BackupTypeId { get; set; }
        public BackupType BackupType { get; set; }

        [ForeignKey("FkAssetId")]
        [Display(Name = "Asset")]
        public long AssetId { get; set; }
        public Asset Asset { get; set; }

    }
}
