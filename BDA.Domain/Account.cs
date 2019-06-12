using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Account : EntityWithTypeId<int>
    {
        public Account()
        {
            AccountOperations = new HashSet<AccountOperation>();
        }
  
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
        public Contract Contract { get; set; }
        public ICollection<AccountOperation> AccountOperations { get; set; }
    }
}