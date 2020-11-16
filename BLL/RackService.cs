using BLL.interfaces;
using DAL.interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RackService: IRackService
    {
        readonly IRackRepository repository;
        readonly IRackLocationRepository rackLocationRepository;

        public RackService(IRackRepository _repository, IRackLocationRepository _rackLocationRepository)
        {
            repository = _repository;
            rackLocationRepository = _rackLocationRepository;
        }

        public List<Rack> GetAllRacks()
        {
            return repository.GetAllRacks();
        }

        public Tuple<long, Rack, List<RackLocation>> GetAllRacksWithRackLocations(long rackID)
        {
            Rack rack = FindById(rackID);

            List<RackLocation> rackLocations = rackLocationRepository.GetAllRackLocationsOfRack(rackID);

            return new Tuple<long, Rack, List<RackLocation>>(rackID, rack, rackLocations);
        }

        public Rack FindById(long id)
        {
            return repository.FindById(id);
        }

        public bool RackExists(long id)
        {
            return repository.RackExists(id);
        }

        public void Update(Rack rack)
        {
            repository.Update(rack);
        }

        public void Add(Rack rack)
        {
            repository.Add(rack);
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
