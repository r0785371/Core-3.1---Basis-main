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
    public class DetailMainService: IDetailMainService
    {
        readonly IDetailMainRepository repository;
        readonly IDetailRepository repositoryDetail;

        public DetailMainService(IDetailMainRepository _repository, IDetailRepository _repositoryDetail)
        {
            repository = _repository;
            repositoryDetail = _repositoryDetail;
        }

        public List<DetailMain> GetAllDetailMains()
        {
            return repository.GetAllDetailMains();
        }

        public Tuple<long, DetailMain, List<Detail>> GetAllDetailMainWithDetails(long detailMainID)
        {
            DetailMain detailMain = FindById(detailMainID);

            List<Detail> details = repositoryDetail.GetAllDetailsOfDetailMain(detailMainID);

            return new Tuple<long, DetailMain, List<Detail>>(detailMainID, detailMain, details);
        }

        public DetailMain FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool DetailMainExists(long id)
        {
            return repository.DetailMainExists(id);
        }

        public void Update(DetailMain detailMain)
        {
            repository.Update(detailMain);
        }

        public void Add(DetailMain detailMain)
        {
            repository.Add(detailMain);
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
