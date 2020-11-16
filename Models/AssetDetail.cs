using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class AssetDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AssetDetailID { get; set; }

        [Display(Name = "Asset")]
        public long AssetID { get; set; }
        public Asset Asset { get; set; }

        [Display(Name = "Detail")]
        public long DetailID { get; set; }
        public Detail Detail { get; set; }

        [Required]
        [Display(Name = "Definition 1")]
        public string Definition1 { get; set; }

        [Display(Name = "Definition 2")]
        public string Definition2 { get; set; }
    }
}
