using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Deposit : EntityWithTypeId<int>
    {
        public Deposit()
        {
            Contracts = new HashSet<Contract>();
        }

        public string DepositName { get; set; }
        public int PaymentPeriod { get; set; }
        public int DepositTermId { get; set; }
        public decimal Rate { get; set; }
        public bool AccountReplenishment { get; set; }
        
        public Currency Currency { get; set; }
        public DepositTerm DepositTerm { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}