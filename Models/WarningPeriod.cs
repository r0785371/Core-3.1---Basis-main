using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class WarningPeriod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WarningPeriodID { get; set; }

        //[Index(IsUnique=true)]
        public int WarningPeriodMonth { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        //[Index(IsUnique=true)]
        public bool Standard { get; set; }

        public List<ProductType> ProductTypes { get; set; }

        public List<Asset> Assets { get; set; }
    }
}
