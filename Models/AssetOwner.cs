//using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class AssetOwner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long AssetOwnerID { get; set; }

        [Display(Name = "OperationalSite")]
        public long? OperationalSiteId { get; set; }
        public OperationalSite OperationalSite { get; set; } //= new OperationalSite();

        [Display(Name = "Warehouse")]
        public long? WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } //= new Warehouse();

        [Display(Name = "Person")]
        public long? PersonId { get; set; }
        public Person People { get; set; } //= new Person();

        [Display(Name = "Group of people")]
        public long? GroupPeopleId { get; set; }
        public GroupPeople GroupPeople { get; set; } //= new GroupPeople();

        [Display(Name = "Extern company")]
        public long? ExternCompanyId { get; set; }
        public ExternCompany ExternCompany { get; set; }

        [NotMapped]
        [Display(Name = "Owner type")]
        public string OwnerType
        {
            get
            {
                // Very important to check first if each (foreign) class is not null otherwise it crash!
                // Actually each time can only one of them have a connection!!!
                string name = "";

                if (OperationalSite != null)
                {
                    name = "Operational Site";
                }
                if (Warehouse != null)
                {
                    name = "Warehouse";
                }
                if (People != null)
                {
                    name = "Person";
                }
                if (GroupPeople != null)
                {
                    name = "Group people";
                }
                if (ExternCompany != null)
                {
                    name = "Extern company";
                }

                return name;
            }
        }

        [NotMapped]
        [Display(Name = "Owner")]
        public string OwnerDescription
        {
            get
            {
                // Very important to check first if each (foreign) class is not null otherwise it crash!
                // Actually each time can only one of them have a connection!!!
                string name = "";

                if (OperationalSite != null)
                {
                    name = OperationalSite.Ref;
                }
                if (Warehouse != null)
                {
                    name = Warehouse.Ref;
                }
                if (People != null)
                {
                    name = People.Ref;
                }
                if (GroupPeople != null)
                {
                    name = GroupPeople.Ref;
                }
                if (ExternCompany != null)
                {
                    name = "*" + ExternCompany.Ref;
                }

                return name;
            }
        }
    }
}
