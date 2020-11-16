using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using BLL.interfaces;
using DAL.interfaces;

namespace BLL
{
    public class SoftwareTypeService: ISoftwareTypeService
    {
        readonly ISoftwareTypeRepository repository;
        readonly ISoftwareRepository repositorySoftware;

        public SoftwareTypeService(ISoftwareTypeRepository _repository, ISoftwareRepository _repositorySoftware)
        {
            repository = _repository;
            repositorySoftware = _repositorySoftware;
        }

        public List<SoftwareType> GetAllSoftwareTypes()
        {
            return repository.GetAllSoftwareTypes();
        }

        public Tuple<long, SoftwareType, List<Software>> GetAllSoftwareTypesWithSoftware(long softwareTypeID)
        {
            SoftwareType softwareType = FindById(softwareTypeID);

            List<Software> softwares = repositorySoftware.GetAllSoftwareOfSoftwareType(softwareTypeID);

            return new Tuple<long, SoftwareType, List<Software>>(softwareTypeID, softwareType, softwares);
        }

        public SoftwareType FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool SoftwareTypeExists(long id)
        {
            return repository.SoftwareTypeExists(id);
        }

        public void Update(SoftwareType assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(SoftwareType assetDetail)
        {
            repository.Add(assetDetail);
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
