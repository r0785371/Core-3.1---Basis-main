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
    public class DetailSubService: IDetailSubService
    {
        readonly IDetailSubRepository repository;
        readonly IDetailRepository repositoryDetail;

        public DetailSubService(IDetailSubRepository _repository, IDetailRepository _repositoryDetail)
        {
            repository = _repository;
            repositoryDetail = _repositoryDetail;
        }

        public List<DetailSub> GetAllDetailSubs()
        {
            return repository.GetAllDetailSubs();
        }

        public Tuple<long, DetailSub, List<Detail>> GetAllDetailSubWithDetails(long detailSubID)
        {
            DetailSub detailSub = FindById(detailSubID);

            List<Detail> details = repositoryDetail.GetAllDetailsOfDetailSub(detailSubID);

            return new Tuple<long, DetailSub, List<Detail>>(detailSubID, detailSub, details);
        }

        public DetailSub FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool DetailSubExists(long id)
        {
            return repository.DetailSubExists(id);
        }

        public void Update(DetailSub detailSub)
        {
            repository.Update(detailSub);
        }

        public void Add(DetailSub detailSub)
        {
            repository.Add(detailSub);
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
