using PetPeers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetPeers.Repo.Interfaces
{
    public interface IPetService
    {
        List<Pets> GetAllPets();

        List<Pets> GetUserPets(int id);

        void AddPet(Pets p);
        void updatePet(Pets p);
    }
}
