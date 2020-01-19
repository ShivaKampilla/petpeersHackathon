using PetPeers.Models;
using PetPeers.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetPeers.Repo.Service
{
    public class PetService : IPetService
    {
       
        public void AddPet(Pets p)
        {
            var dbContext = new AquanautDBContext();
            dbContext.Pets.Add(p);
            dbContext.SaveChanges();
        }

        public List<Pets> GetAllPets()
        {
           var dbContext = new AquanautDBContext();
            return dbContext.Pets.ToList();
        }

        public List<Pets> GetUserPets(int id) 
        {

            var dbContext = new AquanautDBContext();
            var data = (from pet in dbContext.Pets.Where(x=>x.OwnerId == id.ToString())
                        select new Pets
                        {
                            Name = pet.Name,
                            Age = pet.Age,
                            Place = pet.Place,
                            OwnerId = pet.OwnerId

                        }).ToList();

            return data;
        }

        public void updatePet(Pets p)
        {
            var dbContext = new AquanautDBContext();
            var pet = dbContext.Pets.FirstOrDefault(x => x.Id == p.Id);
            pet.IsAvailable = 0;
            dbContext.SaveChanges();
        }
    }
}
