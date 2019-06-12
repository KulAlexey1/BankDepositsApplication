using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class AccountOperation : EntityWithTypeId<int>
    {
        public int AccountId { get; set; }        
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }

        public AccountOperationType Type { get; set; }
        public Account Account { get; set; }
    }
}