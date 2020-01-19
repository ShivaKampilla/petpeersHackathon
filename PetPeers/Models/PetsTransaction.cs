using System;
using System.Collections.Generic;

namespace PetPeers.Models
{
    public partial class PetsTransaction
    {
        public int Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int TransactionType { get; set; }
        public int UserId { get; set; }
        public int PetId { get; set; }

        public virtual Pets Pet { get; set; }
        public virtual Users User { get; set; }
    }
}
