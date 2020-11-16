using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class PurchaseTypeService: IPurchaseTypeService
    {
        readonly IPurchaseTypeRepository repository;

        public PurchaseTypeService(IPurchaseTypeRepository _repository)
        {
            repository = _repository;
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            return repository.GetAllPurchaseTypes();
        }

        public PurchaseType FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool PurchaseTypeExists(long id)
        {
            return repository.PurchaseTypeExists(id);
        }

        public void Update(PurchaseType purchaseType)
        {
            repository.Update(purchaseType);
        }

        public void Add(PurchaseType purchaseType)
        {
            repository.Add(purchaseType);
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
