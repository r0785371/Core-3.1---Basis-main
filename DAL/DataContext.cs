using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace DAL
{
    public class DataContext : IdentityDbContext<Models.ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Asset> Assets { get; set; }

        public DbSet<AssetHistory> AssetHistories { get; set; }

        public DbSet<AssetLicense> AssetLicenses { get; set; }

        public DbSet<AssetDetail> AssetDetails { get; set; }

        public DbSet<AssetOwner> AssetOwners { get; set; }

        public DbSet<Backup> Backups { get; set; }

        public DbSet<BackupType> BackupTypes { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Detail> Details { get; set; }

        public DbSet<DetailMain> DetailMains { get; set; }

        public DbSet<DetailSub> DetailSubs { get; set; }

        //public DbSet<Employee> Employees { get; set; }

        public DbSet<ExternCompany> ExternCompanies { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<GroupPeople> GroupsPeople { get; set; }

        //Is Product (Hardware is a child of product)
        public DbSet<Hardware> Hardwares { get; set; }

        public DbSet<License> Licenses { get; set; }

        public DbSet<LicenseType> LicenseTypes { get; set; }

        public DbSet<LicenseValidityType> LicenseValidityTypes { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<OperationalSite> OperationalSites { get; set; }

        public DbSet<OperationalSiteLocation> OperationalSiteLocations { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<PersonGroupPeople> PersonGroupPeoples { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; }

        public DbSet<ProductSupplier> ProductSuppliers { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        public DbSet<PurchaseType> PurchaseTypes { get; set; }

        public DbSet<Rack> Racks { get; set; }

        public DbSet<RackLocation> RackLocations { get; set; }

        //public DbSet<Register> Registers { get; set; }

        public DbSet<Room> Rooms { get; set; }

        //Is Product (Hardware is a child of product)
        public DbSet<Software> Software { get; set; }

        public DbSet<SoftwareType> SoftwareTypes { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<URack> URacks { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<WarningPeriod> WarningPeriods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //om het wis gedrag van da database van cascade naar no action 
            //te veranderen. Dit omdat wanneer een parant rol wordt gewist 
            //ook de child rol wordt gewist en dit mag niet.
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            }

            modelBuilder.Entity<OperationalSite>()
                .HasOne(p => p.OperationalSiteGroup)
                .WithMany(p => p.ListOperationalSiteGroups)
                .HasForeignKey(p => p.OperationalSiteGroupId);

            modelBuilder.Entity<ProductType>()
                .HasOne(p => p.ProductTypeGroup)
                .WithMany(p => p.ListProductTypeGroup)
                .HasForeignKey(p => p.ProductTypeGroupID);

            var converter = new ValueConverter<long[], string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val => long.Parse(val)).ToArray());


            modelBuilder
                .Entity<Detail>()
                .Property(e => e.SelectProductTypes)
                .HasConversion(converter);


            var valueComparer = new ValueComparer<long[]>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())));

            modelBuilder
                .Entity<Detail>()
                .Property(e => e.SelectProductTypes)
                .Metadata
                .SetValueComparer(valueComparer);
        }
    }
}
