using System;
using System.Collections.Generic;

namespace PetPeers.Models
{
    public partial class Users
    {
        public Users()
        {
            PetsTransaction = new HashSet<PetsTransaction>();
        }

        public int Id { get; set; }
        public string Userid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<PetsTransaction> PetsTransaction { get; set; }
    }
}
