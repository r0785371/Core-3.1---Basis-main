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
    public class StatusService: IStatusService
    {
        readonly IStatusRepository repository;
        readonly IAssetRepository repositoryAsset;
        readonly IHardwareRepository repositoryHardware;
        readonly ILicenseRepository repositoryLicense;
        readonly IPurchaseItemRepository repositoryPurchaseItem;
        readonly ISoftwareRepository repositorySoftware;
        
        public StatusService(IStatusRepository _repository, IAssetRepository _repositoryAsset, 
                                IHardwareRepository _repositoryHardware, ILicenseRepository _repositoryLicense,
                                IPurchaseItemRepository _repositoryPurchaseItem, ISoftwareRepository _repositorySoftware)
        {
            repository = _repository;
            repositoryAsset = _repositoryAsset;
            repositoryHardware = _repositoryHardware;
            repositoryLicense = _repositoryLicense;
            repositoryPurchaseItem = _repositoryPurchaseItem;
            repositorySoftware = _repositorySoftware;
        }

        public List<Status> GetAllStatus()
        {
            return repository.GetAllStatus();
        }

        public Status FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool StatusExists(long id)
        {
            return repository.StatusExists(id);
        }

        public void Update(Status assetDetail)
        {
            repository.Update(assetDetail);
        }

        public void Add(Status assetDetail)
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

        public Tuple<long, Status, List<Hardware>, List<Software>, List<PurchaseItem>, List<License>, List<Asset>> GetStatusWithRelatedSubs(long statusID)
        {
            Status status = FindById(statusID);

            List<Hardware> hardwares = repositoryHardware.GetAllHardwareOfStatus(statusID);

            List<Software> softwares = repositorySoftware.GetAllSoftwareOfStatus(statusID);

            List<PurchaseItem> purchaseItems = repositoryPurchaseItem.GetAllPurchaseItemsOfStatus(statusID);

            List<License> licenses = repositoryLicense.GetAllLicensesOfStatus(statusID);

            List<Asset> assets = repositoryAsset.GetAllAssetsOfStatus(statusID);

            return new Tuple<long, Status, List<Hardware>, List<Software>, List<PurchaseItem>, List<License>, List<Asset>>(statusID, status, hardwares, softwares, purchaseItems, licenses, assets);
        }
    }
}
