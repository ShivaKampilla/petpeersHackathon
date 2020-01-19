using PetPeers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetPeers.Repo.Interfaces
{
    public interface IUserService
    {
        bool validateUser(Users u);
        bool addUser(Users u);
    }
}
