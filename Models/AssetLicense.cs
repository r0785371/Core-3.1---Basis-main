using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class AssetLicense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AssetLicenseID { get; set; }

        public long AssetID { get; set; }
        public Asset Asset { get; set; }

        public long LicenseID { get; set; }
        public License License { get; set; }




    }
}
