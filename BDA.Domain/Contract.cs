using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Contract : EntityWithTypeId<int>
    {
        public int DepositId { get; set; }
        public int DepositorId { get; set; }
        public int EmployeeId { get; set; }
        public int AccountId { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public Account Account { get; set; }
        public Deposit Deposit { get; set; }
        public Depositor Depositor { get; set; }
        public Employee Employee { get; set; }
    }
}