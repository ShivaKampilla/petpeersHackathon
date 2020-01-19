using PetPeers.Models;
using PetPeers.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetPeers.Repo.Service
{
    public class UserService : IUserService
    {
        public bool addUser(Users u)
        {

            try
            {

                var dbContext = new AquanautDBContext();
                var userExists = dbContext.Users.FirstOrDefault(x => x.Userid ==u.Userid);
                if (userExists == null)
                {
                    dbContext.Users.Add(u);
                    dbContext.SaveChanges();
                    return true;

                }
                else return false;

            }
            catch (Exception ex)
            {
                Startup.Logger?.Error($"Error getting Pet {ex.Message}");
                throw;
            }
           

          
        }

        public bool validateUser(Users u)
        {
            try
            {
                var dbContext = new AquanautDBContext();
                var user = dbContext.Users.FirstOrDefault(x => x.Password == x.Password && x.Userid == u.Userid);
                return user != null ? true : false;
            }
            catch (Exception ex)
            {
                Startup.Logger?.Error($"Error getting Pet {ex.Message}");
                throw;
            }

           
        }
    }
}
