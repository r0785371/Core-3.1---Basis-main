using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Buildings.Any())
            {
                return; //DB has been seeded
            }

            var buildings = new Building[]
            {
                new Building { Ref = "GD002", Name = "Siberiabrug Bedieningsgebouw", Street = "Siberiastraat(haven)", Number = "62", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD066", Name = "VCS Dienstgebouw INFR/ MI", Street = "Kauwenstein(haven)", Number = "4", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD067", Name = "VCS Bedieningsgebouw", Street = "Ordamdijkweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD075", Name = "Noordkasteelbrug Bedieningsgebouw", Street = "Oosterweelsteenweg", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD082", Name = "Wilmarsdonkbrug Bedieningsgebouw", Street = "Wilmarsdonksteenweg(haven)", Number = "205", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD135", Name = "ROS Bedieningsgebouw", Street = "Letlandstraat(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD145", Name = "BOS Bedieningsgebouw Deur 3 en 4", Street = "Boudewijnweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD153", Name = "Petroleumbrug Bedieningsgebouw", Street = "Polderdijkweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD166", Name = "ROS Bureel", Street = "Letlandstraat(haven)", Number = "", Postcode ="2030", City="Antwerpen" },
                new Building { Ref = "GD200", Name = "ZAS Blok A", Street = "Potpolderweg(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD220", Name = "ZAS Blok C", Street = "Potpolderweg(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD223", Name = "ZAS Blok B", Street = "Potpolderweg(haven)", Number = "10", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD241", Name = "ZAS Mechanismegebouw Deur 1 en 2", Street = "Potpolderweg(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD243", Name = "Frederik Hendrikbrug Bedieningsgebouw(MAZ 13)", Street = "Oudendijkwegn(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD244", Name = "ZAS Mechanismegebouw Deur 3 en 4", Street = "Potpolderweg(haven)" , Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD246", Name = "BES Mechanismegebouw Deur 1 en 2", Street = "Berendrechtweg(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD247", Name = "Oudendijkbrug Bedieningsgebouw(MAB 4)", Street = "Oudendijkweg", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD248", Name = "BES Mechanismegebouw Deur 3 en 4", Street = "Berendrechtweg(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD249", Name = "Berendrechtbrug Bedieningsgebouw(MAB 12)", Street = "Scheldelaan(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD250", Name = "KAS Dienstgebouw Steenlandlaan(haven)", Number = "", Postcode = "9130", City = "Kallo" },
                new Building { Ref = "GD252", Name = "KAS Mechanismegebouw Deur 1 en 2", Street = "Steenlandlaan(haven)", Number = "", Postcode = "9130", City = "Kallo"},
                new Building { Ref = "GD253", Name = "KAS Mechanismegebouw Deur 3 en 4", Street = "Ketenislaan(haven)", Postcode = "9130", City = "Kallo" },
                new Building { Ref = "GD255", Name = "BOS Bedieningsgebouw Deur 1 en 2", Street = "Boudewijnweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD256", Name = "VCS Centraal Dienstgebouw", Street = "Kauwenstein(haven)", Number = "7", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD258", Name = "ZAS Blok D", Street = "Potpolderweg(haven)", Number = "6", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD266", Name = "Lillobrug Oost Bedieningsgebouw", Street = "Lillobrug(haven)", Number ="", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD267", Name = "Lillobrug West Bruggebouw", Street = "Lillobrug(haven)", Number = "", Postcode = "2040", City = "Antwerpen" },
                new Building { Ref = "GD300", Name = "Oosterweelbrug Bedieningsgebouw", Street = "Oosterweelsteenweg(haven)", Number = "135", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD440", Name = "VCS mechanismegebouw Deur 3 - 4", Street = "Ordamdijkweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD445", Name = "Datacenter Thornton Road", Street = "Thorntonroad", Number = "zn", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD446", Name = "BOS decentraal eiland BOS - D1 - 2 Z", Street = "Kauwenstein(haven)", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD447", Name = "BOS decentraal eiland BOS - B1 - N", Street = "Kruisschansweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD448", Name = "BOS decentraal eiland BOS - B2 - Z", Street = "Kauwenstein(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD449", Name = "BOS decentraal eiland BOS - SS - Z", Street = "Kruisschansweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD464", Name = "VCS decentraal eiland VCS - D1 - 2 N", Street = "Kauwenstein(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD465", Name = "VCS decentraal eiland VCS - ZW", Street = "Kruisschansweg(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD466", Name = "VCS decentraal eiland VCS - B2 - N", Street = "Kauwenstein(haven)", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "GD474", Name = "KIS Blok A Bedieningsgebouw", Street = "Sint Antoniusweg(Haven)", Number = "1620", Postcode = "9130", City = "Beveren" },
                new Building { Ref = "GD475", Name = "KIS Blok B EMU gebouw", Street = "Sint Antoniusweg(Haven)", Number = "1620", Postcode = "9130", City = "Beveren" },
                new Building { Ref = "GD477", Name = "KIS Blok D’ Mechanismegebouw deur 1 en 2", Street = "Sint Antoniusweg(Haven)", Number = "1620", Postcode = "9130", City = "Beveren" },
                new Building { Ref = "GD478", Name = "KIS Blok D Mechanismegebouw deur 3 en 4", Street = "Sint Antoniusweg(Haven)", Number = "1620", Postcode = "9130", City = "Beveren" },
                new Building { Ref = "GD482", Name = "KIS Blok G Bruggebouw Waaslandkanaal", Street = "Sint Antoniusweg(Haven)", Number = "1620", Postcode = "9130", City = "Beveren" },
                new Building { Ref = "GD483", Name = "KIS Blok J Bruggebouw Deurganckdok", Street = "Sint Antoniusweg(Haven)", Number = "1620", Postcode = "9130", City = "Beveren" },
                new Building { Ref = "GD507", Name = "Datacenter 2", Street = "Loodglansstraat", Number = "", Postcode = "2030", City = "Antwerpen" },
                new Building { Ref = "BU001", Name = "Bebouw A", Street = "Kruisschansweg", Number = "10", Postcode = "2000", City = "Antwerpen" },
                new Building { Ref = "BU002", Name = "Bebouw B", Street = "Havenweg", Number = "18", Postcode = "2100", City = "Antwerpen" },
                new Building { Ref = "BU003", Name = "Bebouw C", Street = "Lierselaan", Number = "55", Postcode = "2000", City = "Antwerpen" },
                new Building { Ref = "BU004", Name = "Bebouw D", Street = "Mechelsesteenweg", Number = "50", Postcode = "2000", City = "Antwerpen" },
                new Building { Ref = "BU005", Name = "Bebouw E", Street = "Paterstraat", Number = "100", Postcode = "2000", City = "Antwerpen" },

            };

            foreach (Building b in buildings)
            {
                context.Buildings.Add(b);
            }
            context.SaveChanges();


            //Floors
            if (context.Floors.Any())
            {
                return; //DB has been seeded
            }

            var floors = new Floor[]
            {
                new Floor{ Ref = "-1", Name = "Garage"},
                new Floor{ Ref = "0", Name = "0 Level"},
                new Floor{ Ref = "1", Name = "1e verdieping"},
                new Floor{ Ref = "2", Name = "2e verdieping"},
                new Floor{ Ref = "3", Name = "3e verdieping"},
                new Floor{ Ref = "4", Name = "4e verdieping"},
                new Floor{ Ref = "5", Name = "5e verdieping"},
            };

            foreach (Floor f in floors)
            {
                context.Floors.Add(f);
            }
            context.SaveChanges();

            //Rooms
            if (context.Rooms.Any())
            {
                return; //DB has been seeded
            }

            var rooms = new Room[]
            {
                new Room{ Ref = "BL", Name = "Bedieningslokaal"},
                new Room{ Ref = "DC", Name = "Data center"},
                new Room{ Ref = "ALSB", Name = "Laagspanningspost"},
                new Room{ Ref = "LABO", Name = "Labo"},
                new Room{ Ref = "MAG", Name = "Magazijn"},
                new Room{ Ref = "MCK", Name = "Machinekamer"},
                new Room{ Ref = "SR", Name = "Server ruimte"},
                new Room{ Ref = "GTR", Name = "Technische ruimte"},
            };

            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();

            ////Locations
            //if (context.Locations.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var locations = new Location[]
            //{
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU001").BuildingID , FloorId = floors.Single(f => f.Ref == "0").FloorID, 
            //        RoomId =  rooms.Single(r => r.Ref == "SR").RoomID, RoomNo = "SR001", IsWarehouse = false},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU001").BuildingID , FloorId = floors.Single(f => f.Ref == "1").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "SR").RoomID, RoomNo = "SR002", IsWarehouse = false},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU002").BuildingID , FloorId = floors.Single(f => f.Ref == "0").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "MG").RoomID, RoomNo = "MG001", IsWarehouse = true},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU003").BuildingID , FloorId = floors.Single(f => f.Ref == "0").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "DC").RoomID, RoomNo = "DC001", IsWarehouse = false},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU004").BuildingID , FloorId = floors.Single(f => f.Ref == "2").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "SR").RoomID, RoomNo = "SR001", IsWarehouse = false},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU004").BuildingID , FloorId = floors.Single(f => f.Ref == "0").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "SR").RoomID, RoomNo = "SR002", IsWarehouse = true},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU005").BuildingID , FloorId = floors.Single(f => f.Ref == "0").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "SR").RoomID, RoomNo = "SR001", IsWarehouse = false},
            //    new Location{ BuildingId = buildings.Single(b => b.Ref == "BU005").BuildingID , FloorId = floors.Single(f => f.Ref == "2").FloorID,
            //        RoomId =  rooms.Single(r => r.Ref == "SR").RoomID, RoomNo = "SR002", IsWarehouse = false},
            //};

            //foreach (Location l in locations)
            //{
            //    context.Locations.Add(l);
            //}
            //context.SaveChanges();

            //Racks
            if (context.Racks.Any())
            {
                return; //DB has been seeded
            }

            var racks = new Rack[]
            {
                new Rack{ Name = "Rack 1"},
                new Rack{ Name = "Rack 2"},
                new Rack{ Name = "Rack 3"},
                new Rack{ Name = "Rack 4"},
                new Rack{ Name = "Rack 5"},
                new Rack{ Name = "Rack 6"},
                new Rack{ Name = "Rack 7"},
                new Rack{ Name = "Rack 8"},
                new Rack{ Name = "Rack 9"},
                new Rack{ Name = "Rack 10"},
                new Rack{ Name = "Rack 11"},
                new Rack{ Name = "Rack 12"},
                new Rack{ Name = "Rack 13"},
                new Rack{ Name = "Rack 14"},
                new Rack{ Name = "Rack 15"},
            };

            foreach (Rack r in racks)
            {
                context.Racks.Add(r);
            }
            context.SaveChanges();

            //URacks
            if (context.URacks.Any())
            {
                return; //DB has been seeded
            }

            var uRacks = new URack[]
            {
                new URack{ Name = "U-Rack 1"},
                new URack{ Name = "U-Rack 2"},
                new URack{ Name = "U-Rack 3"},
                new URack{ Name = "U-Rack 4"},
                new URack{ Name = "U-Rack 5"},
                new URack{ Name = "U-Rack 6"},
                new URack{ Name = "U-Rack 7"},
                new URack{ Name = "U-Rack 8"},
                new URack{ Name = "U-Rack 9"},
                new URack{ Name = "U-Rack 10"},
                new URack{ Name = "U-Rack 11"},
                new URack{ Name = "U-Rack 12"},
                new URack{ Name = "U-Rack 13"},
                new URack{ Name = "U-Rack 14"},
                new URack{ Name = "U-Rack 15"},
                new URack{ Name = "U-Rack 16"},
                new URack{ Name = "U-Rack 17"},
                new URack{ Name = "U-Rack 18"},
                new URack{ Name = "U-Rack 19"},
                new URack{ Name = "U-Rack 20"},
                new URack{ Name = "U-Rack 21"},
                new URack{ Name = "U-Rack 22"},
                new URack{ Name = "U-Rack 23"},
                new URack{ Name = "U-Rack 24"},
                new URack{ Name = "U-Rack 25"},
                new URack{ Name = "U-Rack 26"},
                new URack{ Name = "U-Rack 27"},
                new URack{ Name = "U-Rack 28"},
                new URack{ Name = "U-Rack 29"},
                new URack{ Name = "U-Rack 30"},
                new URack{ Name = "U-Rack 31"},
                new URack{ Name = "U-Rack 32"},
                new URack{ Name = "U-Rack 33"},
                new URack{ Name = "U-Rack 34"},
                new URack{ Name = "U-Rack 35"},
                new URack{ Name = "U-Rack 36"},
                new URack{ Name = "U-Rack 37"},
                new URack{ Name = "U-Rack 38"},
                new URack{ Name = "U-Rack 39"},
                new URack{ Name = "U-Rack 40"},
                new URack{ Name = "U-Rack 41"},
                new URack{ Name = "U-Rack 42"},
            };

            foreach (URack r in uRacks)
            {
                context.URacks.Add(r);
            }
            context.SaveChanges();

            ////RackLocation
            //if (context.RackLocations.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var rackLocations = new RackLocation[]
            //{
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "0" && l.RoomNo == "SR001").LocationID, 
            //        RackID = racks.Single(r => r.Name == "Rack 1").RackID, RackNo = "Rack 01"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "0" && l.RoomNo == "SR001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 2").RackID, RackNo = "Rack 02"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "0" && l.RoomNo == "SR001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 3").RackID, RackNo = "Rack 03"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "0" && l.RoomNo == "SR001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 4").RackID, RackNo = "Rack 04"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "1" && l.RoomNo == "SR002").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 1").RackID, RackNo = "Rack 01"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "1" && l.RoomNo == "SR002").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 2").RackID, RackNo = "Rack 02"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "1" && l.RoomNo == "SR002").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 3").RackID, RackNo = "Rack 03"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "1" && l.RoomNo == "SR002").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 4").RackID, RackNo = "Rack 04"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU001" && l.Floor.Ref == "1" && l.RoomNo == "SR002").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 5").RackID, RackNo = "Rack 05"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU003" && l.Floor.Ref == "0" && l.RoomNo == "DC001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 1").RackID, RackNo = "Rack 01"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU003" && l.Floor.Ref == "0" && l.RoomNo == "DC001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 2").RackID, RackNo = "Rack 02"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU003" && l.Floor.Ref == "0" && l.RoomNo == "DC001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 3").RackID, RackNo = "Rack 03"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU003" && l.Floor.Ref == "0" && l.RoomNo == "DC001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 4").RackID, RackNo = "Rack 04"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU003" && l.Floor.Ref == "0" && l.RoomNo == "DC001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 5").RackID, RackNo = "Rack 05"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU003" && l.Floor.Ref == "0" && l.RoomNo == "DC001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 6").RackID, RackNo = "Rack 06"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU004" && l.Floor.Ref == "2" && l.RoomNo == "SR001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 1").RackID, RackNo = "Rack 01"},
            //    new RackLocation{ LocationID = locations.Single(l => l.Building.Ref == "BU004" && l.Floor.Ref == "2" && l.RoomNo == "SR001").LocationID,
            //        RackID = racks.Single(r => r.Name == "Rack 2").RackID, RackNo = "Rack 02"},
            //};

            //foreach (RackLocation r in rackLocations)
            //{
            //    context.RackLocations.Add(r);
            //}
            //context.SaveChanges();

            //Departments
            if (context.Departments.Any())
            {
                return; //DB has been seeded
            }

            var departments = new Department[]
            {
                new Department{ Name = "Technisch dienst"},
                new Department{ Name = "INFR/MI/AAT"},
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            //GroupPeople
            if (context.GroupsPeople.Any())
            {
                return; //DB has been seeded
            }

            var groupPeople = new GroupPeople[]
            {
                new GroupPeople{Ref="GP-A", GroupName = "Group A"},
                new GroupPeople{Ref="GP-B", GroupName = "Group B"},
                new GroupPeople{Ref="GP-C", GroupName = "Group C"},
                new GroupPeople{Ref="GP-D", GroupName = "Group D"},
            };

            foreach (GroupPeople g in groupPeople)
            {
                context.GroupsPeople.Add(g);
            }
            context.SaveChanges();

            //People
            if (context.People.Any())
            {
                return; //DB has been seeded
            }

            var people = new Person[]
            {
                new Person{Ref = "ADC" , FirstName = "Andrew", LastName = "De Cock", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID },
                new Person{Ref = "BC", FirstName = "Benny", LastName = "Cooreman", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "MR", FirstName = "Maxim", LastName = "Rooman", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "OB", FirstName = "Önder", LastName = "Bagcicek", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "RvH", FirstName = "Raf", LastName = "Van Hekken", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "RA", FirstName = "Raf", LastName = "Adriaenssens", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "RB", FirstName = "Robin", LastName = "Borghys", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "WJ", FirstName = "Werner", LastName = "Janssens", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
                new Person{Ref = "WP", FirstName = "Wim", LastName = "Poppeliers", Phone ="", Email = "", DepartmentId = departments.Single(d => d.Name == "Technisch dienst").DepartmentID},
            };

            foreach (Person p in people)
            {
                context.People.Add(p);
            }
            context.SaveChanges();


            //Warehouse
            if (context.Warehouses.Any())
            {
                return; //DB has been seeded
            }

            //var warehouses = new Warehouse[]
            //{
            //    new Warehouse{Ref="2210-65", Name = "2210-65 Nagazijn Kauwenstein", LocationId = locations.Single(l => l.Building.Ref == "BU002" && l.Floor.Ref == "0" && l.RoomNo == "MG001").LocationID},
            //};

            //foreach (Warehouse w in warehouses)
            //{
            //    context.Warehouses.Add(w);
            //}
            //context.SaveChanges();

            //OperationalSites
            if (context.OperationalSites.Any())
            {
                return; //DB has been seeded
            }

            var operationalSites = new OperationalSite[]
            {
                
                new OperationalSite{ Ref = "BOS/VCS", Name = "BOS/VCS", IsGroup = true},
                new OperationalSite{ Ref = "K102", Name = "Datacenter K102", IsGroup = false},
                new OperationalSite{ Ref = "DTR", Name = "Datacenter Thornton Road", IsGroup = false},
                new OperationalSite{ Ref = "Kas", Name = "Kas", IsGroup = false},
                new OperationalSite{ Ref = "Kis", Name = "Kis", IsGroup = false},
                new OperationalSite{ Ref = "Lib", Name = "Lib", IsGroup = false},
                new OperationalSite{ Ref = "Mxb", Name = "Mxb", IsGroup = false},
                new OperationalSite{ Ref = "Oob/Wib", Name = "Oob/Wib", IsGroup = true},
                new OperationalSite{ Ref = "Peb", Name = "Peb", IsGroup = false},
                new OperationalSite{ Ref = "Ros", Name = "Ros", IsGroup = false},
                new OperationalSite{ Ref = "Sib", Name = "Sib", IsGroup = false},
                new OperationalSite{ Ref = "Zas/Bes", Name = "Zas/Bes", IsGroup = true},

            };

            foreach (OperationalSite o in operationalSites)
            {
                context.OperationalSites.Add(o);
            }
            context.SaveChanges();

            var operationalSites2 = new OperationalSite[]
            {
                new OperationalSite{ Ref = "Bes", Name = "Bes", IsGroup = false, OperationalSiteGroupId = operationalSites.Single(o => o.Ref == "Zas/Bes").OperationalSiteID},
                new OperationalSite{ Ref = "Zas", Name = "Zas", IsGroup = false, OperationalSiteGroupId = operationalSites.Single(o => o.Ref == "Zas/Bes").OperationalSiteID},
                new OperationalSite{ Ref = "Bos", Name = "Bos", IsGroup = false, OperationalSiteGroupId = operationalSites.Single(o => o.Ref == "BOS/VCS").OperationalSiteID},
                new OperationalSite{ Ref = "VCS", Name = "VCS", IsGroup = false, OperationalSiteGroupId = operationalSites.Single(o => o.Ref == "BOS/VCS").OperationalSiteID},
                new OperationalSite{ Ref = "Oob", Name = "Oob", IsGroup = false, OperationalSiteGroupId = operationalSites.Single(o => o.Ref == "Oob/Wib").OperationalSiteID},
                new OperationalSite{ Ref = "WIB", Name = "WIB", IsGroup = false, OperationalSiteGroupId = operationalSites.Single(o => o.Ref == "Oob/Wib").OperationalSiteID},
            };

            foreach (OperationalSite o2 in operationalSites2)
            {
                context.OperationalSites.Add(o2);
            }
            context.SaveChanges();


            //AssetOwner
            if (context.AssetOwners.Any())
            {
                return; //DB has been seeded
            }

            var assetOwners = new AssetOwner[]
            {
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "ADC").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "BC").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "MR").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "OB").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "RvH").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "RA").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "RB").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "WJ").PersonID},
                new AssetOwner{ PersonId = people.Single(p => p.Ref == "WP").PersonID},

                new AssetOwner{ GroupPeopleId = groupPeople.Single(g => g.Ref == "GP-A").GroupPeopleID},
                new AssetOwner{ GroupPeopleId = groupPeople.Single(g => g.Ref == "GP-B").GroupPeopleID},
                new AssetOwner{ GroupPeopleId = groupPeople.Single(g => g.Ref == "GP-C").GroupPeopleID},
                new AssetOwner{ GroupPeopleId = groupPeople.Single(g => g.Ref == "GP-D").GroupPeopleID},

                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "BOS/VCS").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "K102").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "DTR").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Kas").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Kis").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Lib").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Mxb").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Oob/Wib").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Peb").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Ros").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Sib").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites.Single(o => o.Ref == "Zas/Bes").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites2.Single(o => o.Ref == "Bes").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites2.Single(o => o.Ref == "Zas").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites2.Single(o => o.Ref == "Bos").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites2.Single(o => o.Ref == "VCS").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites2.Single(o => o.Ref == "Oob").OperationalSiteID},
                new AssetOwner{ OperationalSiteId = operationalSites2.Single(o => o.Ref == "WIB").OperationalSiteID},


                //new AssetOwner{ WarehouseId = warehouses.Single(w => w.Ref == "2210-65").WarehouseID},
            };

            foreach (AssetOwner a in assetOwners)
            {
                context.AssetOwners.Add(a);
            }
            context.SaveChanges();




            //BackupType
            if (context.BackupTypes.Any())
            {
                return; //DB has been seeded
            }

            var backupTypes = new BackupType[]
            {
                new BackupType{ Name = "Dagelijkse"},
                new BackupType{ Name = "Wekelijkse"},
                new BackupType{ Name = "Maandelijkse"},
                new BackupType{ Name = "Jaarlijkse"},
            };

            foreach (BackupType b in backupTypes)
            {
                context.BackupTypes.Add(b);
            }
            context.SaveChanges();


            //DatailMain
            if (context.DetailMains.Any())
            {
                return; //DB has been seeded
            }

            var detailMains = new DetailMain[]
            {
                new DetailMain{ Name = "Batterijen"},
                new DetailMain{ Name = "Firmware"},
                new DetailMain{ Name = "Netwerkkaarten"},
                new DetailMain{ Name = "Opstelling"},
                new DetailMain{ Name = "OS"},
                new DetailMain{ Name = "SFP"},
                new DetailMain{ Name = "Vermogen"},
            };

            foreach (DetailMain b in detailMains)
            {
                context.DetailMains.Add(b);
            }
            context.SaveChanges();

            //DatailSub
            if (context.DetailSubs.Any())
            {
                return; //DB has been seeded
            }

            var detailSubs = new DetailSub[]
            {
                new DetailSub{ Name = "Version"},
                new DetailSub{ Name = "Aantal"},
                new DetailSub{ Name = "Referentie"},
                new DetailSub{ Name = "Type"},
                new DetailSub{ Name = "kVA"},
                new DetailSub{ Name = "Parallel"},
            };

            foreach (DetailSub b in detailSubs)
            {
                context.DetailSubs.Add(b);
            }
            context.SaveChanges();

            ////Datail
            //if (context.Details.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var details = new Detail[]
            //{
            //    //new Detail{ DetailMainID = detailMains.Single(m => m.Name == "").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "").DetailSubID},

            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Dimension(s)").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Lengte").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Dimension(s)").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Breedte").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Dimension(s)").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Hoogte").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Gewicht").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Kg").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Energy supply").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "220V").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Energy supply").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "360V").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Energy supply").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "50 Hz").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Processor").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Type ref.").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Processor").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Aantal").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Processor").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "RAM").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Processor").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Description").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Memory").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Type ref.").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Memory").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "RAM").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Operating system (OS)").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Windows").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Operating system (OS)").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "Linux").DetailSubID},
            //    new Detail{ DetailMainID = detailMains.Single(m => m.Name == "Operating system (OS)").DetailMainID, DetailSubID = detailSubs.Single(s => s.Name == "iOS").DetailSubID},
            //};

            //foreach (Detail b in details)
            //{
            //    context.Details.Add(b);
            //}
            //context.SaveChanges();

            ////SoftwareTypes
            //if (context.SoftwareTypes.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var softwareTypes = new SoftwareType[]
            //{
            //    new SoftwareType{ Name = "Windows"},
            //    new SoftwareType{ Name = "Office"},
            //    new SoftwareType{ Name = "Word"},
            //    new SoftwareType{ Name = "Excel"},
            //};

            //foreach (SoftwareType s in softwareTypes)
            //{
            //    context.SoftwareTypes.Add(s);
            //}
            //context.SaveChanges();

            //LicenseTypes
            if (context.LicenseTypes.Any())
            {
                return; //DB has been seeded
            }

            var licenseTypes = new LicenseType[]
            {
                new LicenseType{ Name = "Single", LimitedUse=true},
                new LicenseType{ Name = "Update", LimitedUse=true},
                new LicenseType{ Name = "Floating", LimitedUse = true, UnlimitedUse = false},
                new LicenseType{Name = "Unlimited use", UnlimitedUse = true}
            };

            foreach (LicenseType l in licenseTypes)
            {
                context.LicenseTypes.Add(l);
            }
            context.SaveChanges();

            //LicenseValidityTypes
            if (context.LicenseValidityTypes.Any())
            {
                return; //DB has been seeded
            }

            var licenseValidityTypes = new LicenseValidityType[]
            {
                new LicenseValidityType{ Name = "Trial"},
                new LicenseValidityType{ Name = "Unlimited"},
                new LicenseValidityType{ Name = "End"},
            };

            foreach (LicenseValidityType l in licenseValidityTypes)
            {
                context.LicenseValidityTypes.Add(l);
            }
            context.SaveChanges();

            //WarningPeriods
            if (context.WarningPeriods.Any())
            {
                return; //DB has been seeded
            }

            var warningPeriods = new WarningPeriod[]
            {
                new WarningPeriod{ Name = "1 month", Description = "Warning 1 month before expire", WarningPeriodMonth = 1, Standard = true},
                new WarningPeriod{ Name = "2 month", Description = "Warning 2 month before expire", WarningPeriodMonth = 2, Standard = false},
                new WarningPeriod{ Name = "3 month", Description = "Warning 3 month before expire", WarningPeriodMonth = 3, Standard = false},
                new WarningPeriod{ Name = "6 month", Description = "Warning 6 month before expire", WarningPeriodMonth = 6, Standard = false},
            };

            foreach (WarningPeriod w in warningPeriods)
            {
                context.WarningPeriods.Add(w);
            }
            context.SaveChanges();

            //PurchaseTypes
            if (context.PurchaseTypes.Any())
            {
                return; //DB has been seeded
            }

            var purchaseTypes = new PurchaseType[]
            {
                new PurchaseType{ Name = "Invoice"},
                new PurchaseType{ Name = "Procurement"},
            };

            foreach (PurchaseType p in purchaseTypes)
            {
                context.PurchaseTypes.Add(p);
            }
            context.SaveChanges();

            //ProductTypes
            if (context.ProductTypes.Any())
            {
                return; //DB has been seeded
            }

            var productTypes = new ProductType[]
            {
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "Comp", Name = "Computer", Description = "Computer (only on Hardware", IsGroup = true, ProductTypeGroupID = null, IsProduct = true, 
                    IsLicense = false, IsAsset = false, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID, 
                    TagNo = "PC", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = false},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "SW", Name = "Switch", Description = "Switch", IsGroup = false, ProductTypeGroupID = null, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "SW", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = true},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "DR", Name = "Drives", Description = "Drive", IsGroup = false, ProductTypeGroupID = null, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = false},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "UP", Name = "UPS", Description = "UPS", IsGroup = false, ProductTypeGroupID = null, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "UP", IsComponent = false, HasBackup = false, BackupInterval = 0, GoInRack = false},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "PL", Name = "PLC", Description = "PLC", IsGroup = false, ProductTypeGroupID = null, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "PL", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = true},
                new ProductType{ ProductChild = ProductChildren.Software, Ref = "LIC", Name = "License", Description = "License", IsGroup = false, ProductTypeGroupID = null, IsProduct = false,
                    IsLicense = true, IsAsset = false, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "LIC", IsComponent = false, HasBackup = false, BackupInterval = 0, GoInRack = false},
                new ProductType{ ProductChild = ProductChildren.Software, Ref = "LS", Name = "License server", Description = "License server", IsGroup = false, ProductTypeGroupID = null, IsProduct = false,
                    IsLicense = true, IsAsset = false, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "LS", IsComponent = false, HasBackup = false, BackupInterval = 0, GoInRack = false},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "CP", Name = "Component", Description = "PLC", IsGroup = false, ProductTypeGroupID = null, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "", IsComponent = true, HasBackup = false, BackupInterval = 0, GoInRack = false},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "LAP", Name = "Laptop", Description = "Laptop", IsGroup = false, ProductTypeGroupID = null, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 1).WarningPeriodID,
                    TagNo = "PC", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = false},
            };

            foreach (ProductType p in productTypes)
            {
                context.ProductTypes.Add(p);
            }
            context.SaveChanges();

            var productTypes2 = new ProductType[]
            {
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "ES", Name = "Eng. station", Description = "Eng. station", IsGroup = false, ProductTypeGroupID = productTypes.Single(p => p.Ref == "Comp").ProductTypeGroupID, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 2).WarningPeriodID,
                    TagNo = "PC", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = true},
                new ProductType{ ProductChild = ProductChildren.Hardware, Ref = "SD", Name = "Scada server", Description = "Scada server", IsGroup = false, ProductTypeGroupID = productTypes.Single(p => p.Ref == "Comp").ProductTypeGroupID, IsProduct = true,
                    IsLicense = false, IsAsset = true, ExpirePeriodMonth = 36, WarningPeriodID = warningPeriods.Single(w => w.WarningPeriodMonth == 2).WarningPeriodID,
                    TagNo = "PC", IsComponent = false, HasBackup = true, BackupInterval = 30, GoInRack = true},
            };

            foreach (ProductType p2 in productTypes2)
            {
                context.ProductTypes.Add(p2);
            }
            context.SaveChanges();

            //Suppliers
            if (context.Suppliers.Any())
            {
                return; //DB has been seeded
            }

            var suppliers = new Supplier[]
            {
                new Supplier{ Name = "Microsoft", Country = "", ServiceDeskWeb = "", IsActief = true, HasHardware = true, HasSoftware = true},
                new Supplier{ Name = "HP", Country = "", ServiceDeskWeb = "", IsActief = true, HasHardware = true, HasSoftware = false},
                new Supplier{ Name = "DEL", Country = "", ServiceDeskWeb = "", IsActief = true, HasHardware = true, HasSoftware = false},
                new Supplier{ Name = "Hirschmann", Country = "", ServiceDeskWeb = "", IsActief = true, HasHardware = true, HasSoftware = false},
            };

            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();


            //Status
            if (context.Status.Any())
            {
                return; //DB has been seeded
            }


            var statuss = new Status[]
            {
                new Status{ Name = "To order", Description = "Need still to be ordered", HasProduct = true, ProductSequence = 1, NoSupport = false,
                    HasPurchase = true, PurchaseSequence = 1, HasAsset = false, AssetSequence = null, HasLicense = false,
                    LicenceSequence = null, GenerateAssetOrLicense = false, ToOrder = true, Ordered = false, OnStock = false, InUse = false, OutOfUse = false},
                new Status{ Name = "Ordered", Description = "...item is ordered (not arrived yet)", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = true, PurchaseSequence = 2, HasAsset = false, AssetSequence = null, HasLicense = false,
                    LicenceSequence = null,  GenerateAssetOrLicense = false, ToOrder = false, Ordered = true, OnStock = false, InUse = false, OutOfUse = false},
                new Status{ Name = "On stock", Description = "...item is on stock (free)", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = true, PurchaseSequence = 3, HasAsset = true, AssetSequence = 1, HasLicense = true,
                    LicenceSequence = 1,  GenerateAssetOrLicense = true, OnStock = true, InUse = false, OutOfUse = false},
                new Status{ Name = "Reserved", Description = "...item is on stock, but already reserved", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 2, HasLicense = true,
                    LicenceSequence = 2,  GenerateAssetOrLicense = true, OnStock = true, InUse = false, OutOfUse = false},
                new Status{ Name = "In use", Description = "...item is in use (working)", HasProduct = true, ProductSequence = 2, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 3, HasLicense = true,
                    LicenceSequence = 3,  GenerateAssetOrLicense = true, OnStock = false, InUse = true, OutOfUse = false},
                new Status{ Name = "At repair", Description = "...item is at repair", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 4, HasLicense = false,
                    LicenceSequence = null,  GenerateAssetOrLicense = true, OnStock = false, InUse = false, OutOfUse = false},
                new Status{ Name = "Test", Description = "...item is being tested.", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 5, HasLicense = false,
                    LicenceSequence = null,  GenerateAssetOrLicense = true, OnStock = false, InUse = false, OutOfUse = false},
                new Status{ Name = "Aan vervanging toe", Description = "...item is in use, but needs to be replaces soon", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 6, HasLicense = true,
                    LicenceSequence = 4,  GenerateAssetOrLicense = true, OnStock = false, InUse = true, OutOfUse = false},
                new Status{ Name = "Recuperatie", Description = "...item is terug op voorraad (dus in gebruik)", HasProduct = false, ProductSequence = null, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 7, HasLicense = false,
                    LicenceSequence = null,  GenerateAssetOrLicense = true, OnStock = true, InUse = false, OutOfUse = false},
                new Status{ Name = "Fase-out", Description = "...item krijgt geen support meer van leverancier", HasProduct = true, ProductSequence = 3, NoSupport = true, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 8, HasLicense = true,
                    LicenceSequence = 5,  GenerateAssetOrLicense = true, OnStock = false, InUse = true, OutOfUse = false},
                new Status{ Name = "Out", Description = "...item is verwijdeerd worden.", HasProduct = true, ProductSequence = 4, NoSupport = false, 
                    HasPurchase = false, PurchaseSequence = null, HasAsset = true, AssetSequence = 9, HasLicense = true,
                    LicenceSequence = 6,  GenerateAssetOrLicense = false, OnStock = false, InUse = false, OutOfUse = true},
            };

            foreach (Status s in statuss)
            {
                context.Status.Add(s);
            }
            context.SaveChanges();


            ////OperationalSiteLocations
            //if (context.OperationalSiteLocations.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var operationalSiteLocations = new OperationalSiteLocation[]
            //{
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites.Single(o => o.Ref == "KW-A").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "SR001" && l.Building.Ref == "BU001" && l.Floor.Ref == "0").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites.Single(o => o.Ref == "KW-B").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "SR002" && l.Building.Ref == "BU001" && l.Floor.Ref == "1").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites.Single(o => o.Ref == "KW-C").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "MG001" && l.Building.Ref == "BU002" && l.Floor.Ref == "0").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites.Single(o => o.Ref == "KW-D").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "DC001" && l.Building.Ref == "BU003" && l.Floor.Ref == "0").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites2.Single(o => o.Ref == "KW-E").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "SR001" && l.Building.Ref == "BU004" && l.Floor.Ref == "2").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites2.Single(o => o.Ref == "KW-F").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "SR002" && l.Building.Ref == "BU004" && l.Floor.Ref == "0").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites.Single(o => o.Ref == "KW-A").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "SR001" && l.Building.Ref == "BU005" && l.Floor.Ref == "0").LocationID},
            //    new OperationalSiteLocation{ OperationalSiteID = operationalSites.Single(o => o.Ref == "KW-D").OperationalSiteID,
            //        LocationID = locations.Single(l => l.RoomNo == "SR002" && l.Building.Ref == "BU005" && l.Floor.Ref == "2").LocationID},
            //};

            //foreach (OperationalSiteLocation o in operationalSiteLocations)
            //{
            //    context.OperationalSiteLocations.Add(o);
            //}
            //context.SaveChanges();


            


            ////Hardware
            //if (context.Products.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var hardwares = new Hardware[]
            // {
            //    new Hardware{ Name = "PC 1", Description = "PC 1", ProductTypeID = productTypes.Single(p => p.Ref == "SD").ProductTypeID, StatusID = statuss.Single(s => s.Name == "In use").StatusID},
            //    new Hardware{ Name = "Laptop 1", Description = "Laptop 1", ProductTypeID = productTypes.Single(p => p.Ref == "LAP").ProductTypeID, StatusID = statuss.Single(s => s.Name == "In use").StatusID},
            //    new Hardware{ Name = "UPS 1", Description = "UPS 1", ProductTypeID = productTypes.Single(p => p.Ref == "UP").ProductTypeID, StatusID = statuss.Single(s => s.Name == "In use").StatusID},
            //    new Hardware{ Name = "Switch 1", Description = "Switch 1", ProductTypeID = productTypes.Single(p => p.Ref == "SW").ProductTypeID, StatusID = statuss.Single(s => s.Name == "In use").StatusID},

            // };

            //foreach (Hardware h in hardwares)
            //{
            //    context.Products.Add(h);
            //}
            //context.SaveChanges();


            ////Software
            //if (context.Software.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var software = new Software[]
            //{
            //    new Software{ Name = "Win 10", Description = "Windows 10", ProductTypeID = productTypes.Single(p => p.Ref == "LIC").ProductTypeID, 
            //                StatusID = statuss.Single(s => s.Name == "In use").StatusID, SoftwareTypeID = softwareTypes.Single(s => s.Name == "Windows").SoftwareTypeID,
            //                SoftwareVersion = "Win 10"},
            //    new Software{ Name = "Office 2019", Description = "Office 2019", ProductTypeID = productTypes.Single(p => p.Ref == "LIC").ProductTypeID,
            //                StatusID = statuss.Single(s => s.Name == "In use").StatusID, SoftwareTypeID = softwareTypes.Single(s => s.Name == "Office").SoftwareTypeID,
            //                SoftwareVersion = "2019"},
            //};

            //foreach (Software s in software)
            //{
            //    context.Software.Add(s);
            //}
            //context.SaveChanges();

            //Employee
            //if (context.Employees.Any())
            //{
            //    return; //DB has been seeded
            //}

            //var employee = new Employee[]
            //{
            //    new Employee{Name="Andrew" , Email="andrew@portofantwerp.com", Afdeling=Dept.INFR_PI, Functie="regeltechnieker" , PhotoPath="noimage.png"},
            //    new Employee{Name="Reinaldo" , Email="reinaldo@portofantwerp.com", Afdeling=Dept.HR, Functie="Afdelingshoofd", PhotoPath="no-photo-available-icon-10.png" },
            //};

            //foreach (Employee e in employee)
            //{
            //    context.Employees.Add(e);
            //}
            //context.SaveChanges();

        }


    }
}
