using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IBackupTypeService
    {
        List<BackupType> GetAllBackupTypes();

        Tuple<long, BackupType, List<Backup>> GetAllBackupTypesWithBackups(long backupTypeID);

        BackupType FindById(long id);

        bool BackupTypeExists(long id);

        void Update(BackupType backupType);

        void Add(BackupType backupType);

        void Remove(long id);

        void Save();
    }
}
