using BLL.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IBackupService
    {
        List<Backup> GetAllBackups();

        List<SelectListItem> GetSelectListBackupTypes();

        List<SelectListItem> GetSelectListPeople();

        Backup FindById(long id);

        bool BackupExists(long id);

        void Update(Backup backup);

        void Add(Backup backup);

        void Remove(long id);

        void Save();

       
    }
}
