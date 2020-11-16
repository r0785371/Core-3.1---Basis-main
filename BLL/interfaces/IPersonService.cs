using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IPersonService
    {
        List<Person> GetAllPersons();

        List<SelectListItem> GetSelectListDepartments();

        List<SelectListItem> GetSelectListGroupPeople();

        Person FindById(long id);

        bool PersonExists(long id);

        void Update(Person person);

        void Add(Person person);

        //void Remove(long id);

        // Return bool => if remove successfull = true otherwise false & int => qtyAssets for this Person (AssetOwner)
        // in case not successfull!
        Tuple<bool,long, int> Remove(long id);

        List<Asset> GetAllAssetsOfAssetOwner(long assetOwnwerID);

        void Save();

        Tuple<long, Person, List<Asset>, List<PersonGroupPeople>> GetPersonWitAssets(long personID);

    }
}
