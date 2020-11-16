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
    public class BackupRepository: IBackupRepository
    {
        readonly DataContext context;

        public BackupRepository(DataContext _context)
        {
            context = _context;
        }

        public List<Backup> GetAllBackups()
        {
            return context.Backups
                .Include(b => b.Asset)
                .Include(b => b.BackupType)
                .Include(b => b.Person)
                .ToList();
        }

        public List<SelectListItem> GetSelectListBackups()
        {
            return context.Backups.Select(s => new SelectListItem
            {
                Value = s.BackupID.ToString(),
                Text = s.BackupType.Name + " " + s.BackupDate,
                //Selected=c.BackupID.Equals(1)
            }).ToList();
        }

        public List<Backup> GetAllBackupsOfBackupType(long backupTypeID)
        {
            return context.Backups
                .Where(b => b.BackupTypeId == backupTypeID)
                .Include(b => b.Asset)
                .Include(b => b.BackupType)
                .Include(b => b.Person)
                .ToList();
        }

        public Backup FindById(long id)
        {
            return context.Backups
                .Where(s => s.BackupID == id)
                .Include(b => b.Asset)
                .Include(b => b.BackupType)
                .Include(b => b.Person)
                .Single();
        }

        public bool BackupExists(long id)
        {
            return context.Backups.Any(s => s.BackupID == id);
        }

        public bool AssetBackupExists(long id)
        {
            return context.Backups.Any(s => s.AssetId == id);
        }

        public void Add(Backup backup)
        {
            context.Backups.Add(backup);
            context.SaveChanges();
        }

        public void Update(Backup backup)
        {
            context.Backups.Update(backup);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var backup = context.Backups.SingleOrDefault(s => s.BackupID == id);
            context.Backups.Remove(backup);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Backup> GetAllBackupsPerAsset(long id)
        {
            return context.Backups
                .Where(s => s.AssetId == id)
                .Include(p => p.BackupType)
                .Include(p => p.Person)
                //.ThenInclude(d => d.Department)
                .ToList();
        }

        public DateTime? AssetLastBackup(long assetID)
        {
            if (AssetBackupExists(assetID)==true)
            {
                return context.Backups
                .Where(b => b.AssetId == assetID)
                .Max(b => b.BackupDate);
            }

            return null;

        }
    }
}
