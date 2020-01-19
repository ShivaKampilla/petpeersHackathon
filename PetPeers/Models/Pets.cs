using System;
using System.Collections.Generic;

namespace PetPeers.Models
{
    public partial class Pets
    {
        public Pets()
        {
            PetsTransaction = new HashSet<PetsTransaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Place { get; set; }
        public int IsAvailable { get; set; }
        public string OwnerId { get; set; }

        public virtual ICollection<PetsTransaction> PetsTransaction { get; set; }
    }
}
