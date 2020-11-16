using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class OperationalSite
    {
        // One OperationalSite is monstly individual OR be a Group and of 2x other operationalSites where 
        // his Assets work for both ones
        public OperationalSite()
        {
            ListOperationalSiteGroups = new HashSet<OperationalSite>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OperationalSiteID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Reference")]
        public string Ref { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Is group?")]
        public bool IsGroup { get; set; }

        //If operationalSite is a group kan have 2x child operationalSiteen.
        //Only a few unique cases.
        [Display(Name = "Group")]
        public long? OperationalSiteGroupId { get; set; }
        public virtual OperationalSite OperationalSiteGroup { get; set; }
        public ICollection<OperationalSite> ListOperationalSiteGroups { get; set; }
                
        [Display(Name = "Asset Owner")]
        public AssetOwner AssetOwer { get; set; }
        //public List<Asset> Assets { get; set; }

        [Display(Name = "Location")]
        public List<OperationalSiteLocation> OperationalSiteLocations { get; set; }

        

    }
}
