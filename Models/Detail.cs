using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Detail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DetailID { get; set; }

        [Display(Name = "Main detail")]
        public long DetailMainID { get; set; }
        public DetailMain DetailMain { get; set; }

        [Display(Name = "Sub detail")]
        public long DetailSubID { get; set; }
        public DetailSub DetailSub { get; set; }

        public List<ProductDetail> ProductDetails { get; set; }
        public List<AssetDetail> AssetDetails { get; set; }

        [Display(Name = "Selected Product Type")]
        public long[] SelectProductTypes { get; set; }
        
        //public MultiSelectList ProductTypes { get; set; }
    }
}
