using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class PersonGroupPeople
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PersonGroupPeopleID { get; set; }

        [Display(Name ="Person")]
        public long PersonID { get; set; }
        [Display(Name = "Person")]
        public Person Person { get; set; }

        [Display(Name = "Group")]
        public long GroupPeopleID { get; set; }
        [Display(Name = "Group")]
        public GroupPeople GroupPeoples { get; set; }

    }
}
