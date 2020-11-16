using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class URackService: IURackService
    {
        readonly IURackRepository repository;
        readonly IAssetRepository assetRepository;

        public URackService(IURackRepository _repository, IAssetRepository _assetRepository)
        {
            repository = _repository;
            assetRepository = _assetRepository;
        }

        public List<URack> GetAllURacks()
        {
            return repository.GetAllURacks();
        }

        public Tuple<long, URack, List<Asset>> GetAllURacksWithAssets(long uRackID)
        {
            URack uRack = FindById(uRackID);

            List<Asset> assets = assetRepository.GetAllAssetsOfUrack(uRackID);

            return new Tuple<long, URack, List<Asset>>(uRackID, uRack, assets);
        }

        public URack FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool URackExists(long id)
        {
            return repository.URackExists(id);
        }

        public void Update(URack uRack)
        {
            repository.Update(uRack);
        }

        public void Add(URack uRack)
        {
            repository.Add(uRack);
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
