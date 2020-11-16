using BLL.interfaces;
using BLL.ViewModels;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BackupService: IBackupService
    {
        readonly IBackupRepository repository;
        readonly IBackupTypeRepository repositoryBackupType;
        readonly IPersonRepository repositoryPerson;

        public BackupService(IBackupRepository _repository, IBackupTypeRepository _repositoryBackupType, 
            IPersonRepository _repositoryPerson)
        {
            repository = _repository;
            repositoryBackupType = _repositoryBackupType;
            repositoryPerson = _repositoryPerson;
        }

        public List<Backup> GetAllBackups()
        {
            return repository.GetAllBackups();
        }

        public List<SelectListItem> GetSelectListBackupTypes()
        {
            return repositoryBackupType.GetSelectListBackupTypes();
        }

        public List<SelectListItem> GetSelectListPeople()
        {
            return repositoryPerson.GetSelectListPeople();
        }

        public Backup FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool BackupExists(long id)
        {
            return repository.BackupExists(id);
        }

        public void Update(Backup backup)
        {
            repository.Update(backup);
        }

        public void Add(Backup backup)
        {
            repository.Add(backup);
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
