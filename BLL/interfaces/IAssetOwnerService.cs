using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IAssetOwnerService
    {
        List<AssetOwner> GetAllAssetOwners();

        AssetOwner FindById(long id);

        bool AssetOwnerExists(long id);

        void Update(AssetOwner assetOwner);

        void Add(AssetOwner assetOwner);

        void Remove(long id);

        void Save();
    }
}
