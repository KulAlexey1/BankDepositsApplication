using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class PhoneNumber : EntityWithTypeId<int>
    {
        public PhoneNumber()
        {
            Depositors = new HashSet<Depositor>();
            Employees = new HashSet<Employee>();
        }

        public int OperatorId { get; set; }
        public int OperatorCode { get; set; }
        public int Number { get; set; }

        public PhoneNumberOperator Operator { get; set; }
        public ICollection<Depositor> Depositors { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}