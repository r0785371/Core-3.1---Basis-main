using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BackupTypeService: IBackupTypeService
    {
        readonly IBackupTypeRepository repository;
        readonly IBackupRepository repositoryBackup;

        public BackupTypeService(IBackupTypeRepository _repository, IBackupRepository _repositoryBackup)
        {
            repository = _repository;
            repositoryBackup = _repositoryBackup;
        }

        public List<BackupType> GetAllBackupTypes()
        {
            return repository.GetAllBackupTypes();
        }

        public Tuple<long, BackupType, List<Backup>> GetAllBackupTypesWithBackups(long backupTypeID)
        {
            BackupType backupType = FindById(backupTypeID);

            List<Backup> backups = repositoryBackup.GetAllBackupsOfBackupType(backupTypeID);

            return new Tuple<long, BackupType, List<Backup>>(backupTypeID, backupType, backups);
        }

        public BackupType FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool BackupTypeExists(long id)
        {
            return repository.BackupTypeExists(id);
        }

        public void Update(BackupType backupType)
        {
            repository.Update(backupType);
        }

        public void Add(BackupType backupType)
        {
            repository.Add(backupType);
        }

        public void Remove(long id)
        {
            repository.Remove(id);
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
