using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class AssetOwnerRepository : IAssetOwnerRepository
    {
        readonly DataContext context;

        public AssetOwnerRepository(DataContext _context)
        {
            context = _context;
        }
        

        public List<AssetOwner> GetAllAssetOwners()
        {
            return context.AssetOwners
                .Include(a => a.GroupPeople)
                .Include(a => a.OperationalSite)
                .Include(a => a.People)
                .Include(a => a.Warehouse)
                .Include(a => a.ExternCompany)
                .ToList();
        }

        public List<SelectListItem> GetSelectListAssetOwners()
        {
            return context.AssetOwners.Select(s => new SelectListItem
            {
                Value = s.AssetOwnerID.ToString(),
                Text = s.OperationalSite.Name + s.People.FullName + s.Warehouse.Name + s.GroupPeople.GroupName + s.ExternCompany.Name,
            }).ToList();
        }

        public AssetOwner GetAssetOwnerOfPerson(long personID)
        {
            return context.AssetOwners
                .Where(a => a.PersonId == personID)
                //.Include(a => a.GroupPeople)
                //.Include(a => a.OperationalSite)
                .Include(a => a.People)
                //.Include(a => a.Warehouse)
                //.Include(a => a.ExternCompany)
                .Single();
        }

        public AssetOwner GetAssetOwnerOfOperationalSite(long operationalSiteID)
        {
            if (AssetOwnerExists(operationalSiteID) == true)
            {
                return context.AssetOwners
                .Where(a => a.OperationalSiteId == operationalSiteID)
                //.Include(a => a.GroupPeople)
                .Include(a => a.OperationalSite)
                //.Include(a => a.People)
                //.Include(a => a.Warehouse)
                //.Include(a => a.ExternCompany)
                .Single();
            }
            return null;
        }

        public AssetOwner GetAssetOwnerOfGroupePeople(long groupPeopleID)
        {
            return context.AssetOwners
                .Where(a => a.GroupPeopleId == groupPeopleID)
                .Include(a => a.GroupPeople)
                //.Include(a => a.OperationalSite)
                //.Include(a => a.People)
                //.Include(a => a.Warehouse)
                //.Include(a => a.ExternCompany)
                .Single();
        }

        public AssetOwner GetAssetOwnerOfWarehouse(long warehouseID)
        {
            return context.AssetOwners
                .Where(a => a.WarehouseId == warehouseID)
                //.Include(a => a.GroupPeople)
                //.Include(a => a.OperationalSite)
                //.Include(a => a.People)
                .Include(a => a.Warehouse)
                //.Include(a => a.ExternCompany)
                .Single();
        }

        public AssetOwner GetAssetOwnerOfExterCompany(long externCompanyID)
        {
            return context.AssetOwners
            .Where(a => a.ExternCompanyId == externCompanyID)
            //.Include(a => a.GroupPeople)
            //.Include(a => a.OperationalSite)
            //.Include(a => a.People)
            //.Include(a => a.Warehouse)
            .Include(a => a.ExternCompany)
            .Single();
        }

        public AssetOwner FindById(long id)
        {
            return context.AssetOwners
                .Where(s => s.AssetOwnerID == id)
                .Include(a => a.GroupPeople)
                .Include(a => a.OperationalSite)
                .Include(a => a.People)
                .Include(a => a.Warehouse)
                .Include(a => a.ExternCompany)
                .Single();
        }


        public bool AssetOwnerExists(long id)
        {
            return context.AssetOwners.Any(s => s.AssetOwnerID == id);
        }

        public void Add(AssetOwner asset)
        {
            context.AssetOwners.Add(asset);
            context.SaveChanges();
        }

        public void Update(AssetOwner asset)
        {
            context.AssetOwners.Update(asset);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var asset = context.AssetOwners.SingleOrDefault(s => s.AssetOwnerID == id);
            context.AssetOwners.Remove(asset);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void AddAssetOwnerGroupPeople(long id)
        {
            AssetOwner assetOwner = new AssetOwner();
            assetOwner.GroupPeopleId = id;

            context.AssetOwners.Add(assetOwner);
            context.SaveChanges();
        }

        public void RemoveAssetOwnerGroupPeople(long id)
        {
            var assetOwner = context.AssetOwners.SingleOrDefault(s => s.GroupPeopleId == id);
            context.AssetOwners.Remove(assetOwner);
            context.SaveChanges();
        }

        public AssetOwner GetAssetOwnerByOperationalSiteId(long id)
        {
            return context.AssetOwners
                .Where(s => s.OperationalSiteId == id)
                .Include(a => a.GroupPeople)
                .Include(a => a.OperationalSite)
                .Include(a => a.People)
                .Include(a => a.Warehouse)
                .Single();
        }

        public void AddAssetOwnerOperationalSite(long id)
        {
            AssetOwner assetOwner = new AssetOwner();
            assetOwner.OperationalSiteId = id;

            context.AssetOwners.Add(assetOwner);
            context.SaveChanges();
        }

        public long AssetOwnerExistOperationalSite(long id)
        {
            long assetOwnerID = 0;

            //Check first if this OperationalSite exists 
            bool existOperationalSite = context.AssetOwners.Any(s => s.OperationalSiteId == id);


            //if yes, returns the AssetOwnerID of this person
            if (existOperationalSite == true)
            {
                AssetOwner assetOwner = context.AssetOwners
                .Where(s => s.OperationalSiteId == id)
                .Single();

                assetOwnerID = assetOwner.AssetOwnerID;
            }

            //otherwise return 0.
            return assetOwnerID;
        }

        public void RemoveAssetOwnerOperationalSite(long id)
        {
            var assetOwner = context.AssetOwners.SingleOrDefault(s => s.OperationalSiteId == id);
            context.AssetOwners.Remove(assetOwner);
            context.SaveChanges();
        }

        public void AddAssetOwnerPerson(long id)
        {
            AssetOwner assetOwner = new AssetOwner();
            assetOwner.PersonId = id;

            context.AssetOwners.Add(assetOwner);
            context.SaveChanges();
        }

        public long AssetOwnerExistPerson(long id)
        {
            long assetOwnerID = 0;

            //Check first if this Person exists 
            bool existPerson = context.AssetOwners.Any(s => s.PersonId == id);


            //if yes, returns the AssetOwnerID of this person
            if (existPerson == true)
            {
                AssetOwner assetOwner = context.AssetOwners
                .Where(s => s.PersonId == id)
                .Single();

                assetOwnerID = assetOwner.AssetOwnerID;
            }

            //otherwise return 0.
            return assetOwnerID;
        }

        public void RemoveAssetOwnerPerson(long id)
        {
            //Check first if there exist this Person in AssetOwners
            bool assetOwnerExistsPerson = context.AssetOwners.Any(s => s.PersonId == id);

            //if yes...
            if (assetOwnerExistsPerson == true)
            {
                var assetOwner = context.AssetOwners.SingleOrDefault(s => s.PersonId == id);
                context.AssetOwners.Remove(assetOwner);
                context.SaveChanges();
            }
        }

        public void AddAssetOwnerWarehouse(long id)
        {
            AssetOwner assetOwner = new AssetOwner();
            assetOwner.WarehouseId = id;

            context.AssetOwners.Add(assetOwner);
            context.SaveChanges();
        }

        public void RemoveAssetOwnerWarehouse(long id)
        {
            var assetOwner = context.AssetOwners.SingleOrDefault(s => s.WarehouseId == id);
            context.AssetOwners.Remove(assetOwner);
            context.SaveChanges();
        }

        public void AddAssetOwnerExternCompany(long id)
        {
            AssetOwner assetOwner = new AssetOwner();
            assetOwner.ExternCompanyId = id;

            context.AssetOwners.Add(assetOwner);
            context.SaveChanges();
        }

        public void RemoveAssetOwnerExternCompany(long id)
        {
            var assetOwner = context.AssetOwners.SingleOrDefault(s => s.ExternCompanyId == id);
            context.AssetOwners.Remove(assetOwner);
            context.SaveChanges();
        }
    }
}
