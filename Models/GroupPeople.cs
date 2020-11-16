using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
     public class GroupPeople
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GroupPeopleID { get; set; }

        [Required]
        [StringLength(10)]
        public string Ref { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        
        [Display(Name = "Person")]
        public long? PersonGroupPeoplesID { get; set; }
        [Display(Name = "Person")]
        public List<PersonGroupPeople> PersonGroupPeoples { get; set; }

        [Display(Name = "Asset Owner")]
        public AssetOwner AssetOwer { get; set; }
        
    }
}
