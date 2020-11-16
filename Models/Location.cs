using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LocationID { get; set; }

        [Display(Name = "Building")]
        public long BuildingId { get; set; }
        public Building Building { get; set; }
        //public Building Building { get; set; } = new Building();

        [Display(Name = "Floor")]
        public long? FloorId { get; set; }
        public Floor Floor { get; set; }
        //public Floor Floor { get; set; } = new Floor();

        [Display(Name = "Room")]
        public long? RoomId { get; set; }
        public Room Room { get; set; }
        //public Room Room { get; set; } = new Room();

        // per gebouw moet de RoomNo uniek zijn!!!
        //[Required]
        [StringLength(20)]
        [Display(Name = "Room number")]
        public string RoomNo { get; set; }

        [Display(Name = "Is warehouse?")]
        public bool IsWarehouse { get; set; }
        
        //[Display(Name = "Operational site")]
        //public long OperationalSiteLocationID { get; set; }
        //public OperationalSiteLocation OperationalSiteLocation { get; set; }
        public List<OperationalSiteLocation> OperationalSiteLocations { get; set; }

        [Display(Name = "Warehouse")]
        public List<Warehouse> Warehouses { get; set; }

        public List<RackLocation> RackLocations { get; set; }



        //Down know why, but by editing the Location creates Error about this!

        //Beschrijving van gebouw met vloer en kamernummer
        [NotMapped]
        public string LocationDescription
        {
            get
            {
                string name = "";

                if (Building != null)
                {
                    name = Building.Ref + " " + Building.Name;
                }
                if (Floor != null)
                {
                    name += " - " + Floor.Ref;
                }
                if (!string.IsNullOrEmpty(RoomNo))
                {
                    name += " - " + RoomNo;
                }
                if (Room != null)
                {
                    name += " " + Room.Name;
                }
                return name;
            }
        }
    }
}
