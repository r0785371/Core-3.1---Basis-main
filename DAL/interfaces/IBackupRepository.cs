using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IBackupRepository
    {
        List<Backup> GetAllBackups();

        List<SelectListItem> GetSelectListBackups();

        List<Backup> GetAllBackupsOfBackupType(long backupTypeID);

        Backup FindById(long id);

        bool BackupExists(long id);

        void Update(Backup backup);

        void Add(Backup backup);

        void Remove(long id);

        void Save();

        List<Backup> GetAllBackupsPerAsset(long id);

        DateTime? AssetLastBackup(long assetID);
    }
}
