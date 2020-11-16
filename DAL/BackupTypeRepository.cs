using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class BackupTypeRepository: IBackupTypeRepository
    {
        readonly DataContext context;

        public BackupTypeRepository(DataContext _context)
        {
            context = _context;
        }

        public List<BackupType> GetAllBackupTypes()
        {
            return context.BackupTypes
                //.Include(a => a.WarningPeriod)
                .ToList();
        }

        public List<SelectListItem> GetSelectListBackupTypes()
        {
            return context.BackupTypes.Select(s => new SelectListItem
            {
                Value = s.BackupTypeID.ToString(),
                Text = s.Name,
                //Selected=c.BackupTypeID.Equals(1)
            }).ToList();
        }

        public BackupType FindById(long id)
        {
            return context.BackupTypes.Where(s => s.BackupTypeID == id).Single();
        }

        public bool BackupTypeExists(long id)
        {
            return context.BackupTypes.Any(s => s.BackupTypeID == id);
        }

        public void Add(BackupType backupType)
        {
            context.BackupTypes.Add(backupType);
            context.SaveChanges();
        }

        public void Update(BackupType backupType)
        {
            context.BackupTypes.Update(backupType);
            context.SaveChanges();
        }

        public void Remove(long id)
        {
            var backupType = context.BackupTypes.SingleOrDefault(s => s.BackupTypeID == id);
            context.BackupTypes.Remove(backupType);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
