using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IBackupTypeRepository
    {
        List<BackupType> GetAllBackupTypes();

        List<SelectListItem> GetSelectListBackupTypes();

        BackupType FindById(long id);

        bool BackupTypeExists(long id);

        void Update(BackupType backupType);

        void Add(BackupType backupType);

        void Remove(long id);

        void Save();
    }
}
