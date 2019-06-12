using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class DepositTerm : EntityWithTypeId<int>
    {
        public int DaysAmount { get; set; }
        public int MonthsAmount { get; set; }
        public int YearsAmount { get; set; }

        public ICollection<Deposit> Deposits { get; set; }
    }
}