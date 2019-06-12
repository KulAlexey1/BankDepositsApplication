using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Address : EntityWithTypeId<int>
    {
        public Address()
        {
            Depositors = new HashSet<Depositor>();
            Employees = new HashSet<Employee>();
        }

        public int StreetId { get; set; }
        public int House { get; set; }
        public string Housing { get; set; }
        public int Apartment { get; set; }

        public Street Street { get; set; }
        public ICollection<Depositor> Depositors { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}