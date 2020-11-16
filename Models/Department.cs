using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DepartmentID { get; set; }

        [Required]
        [StringLength(25)]
        public string  Name { get; set; }

        [Display(Name ="People")]
        public List<Person>People { get; set; }

    }
}
