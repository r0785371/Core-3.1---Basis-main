using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IStatusRepository
    {
        List<Status> GetAllStatus();

        //List<SelectListItem> GetSelectListStatus();

        List<SelectListItem> GetSelectListStatusProduct();

        List<SelectListItem> GetSelectListStatusPurchase();

        List<SelectListItem> GetSelectListStatusAsset();

        List<SelectListItem> GetSelectListStatusLicense();

        Status FindById(long id);

        long FindLicenseFirstOnStockID();

        long FindAssetFirstOnStockID();

        bool StatusExists(long id);

        void Update(Status status);

        void Add(Status status);

        void Remove(long id);

        void Save();

        
    }
}
