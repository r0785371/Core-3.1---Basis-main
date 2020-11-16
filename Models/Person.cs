using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PersonID { get; set; }

        [Required]
        [StringLength(10)]
        public string Ref { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Phone]
        [StringLength(50)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Department")]
        public long DepartmentId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Group")]
        public long? PersonGroupPeoplesID { get; set; }
        [Display(Name = "Group")]
        public List<PersonGroupPeople> PersonGroupPeoples { get; set; }

        [NotMapped]
        [Display(Name = "Backups")]
        public long? BackUpId { get; set; }
        public List<Backup> Backups { get; set; }

        [Display(Name = "Asset Owner")]
        public AssetOwner AssetOwer { get; set; }

    }
}
