using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Employee : EntityWithTypeId<int>
    {
        public Employee()
        {
            Contracts = new HashSet<Contract>();
        }

        public int PassportId { get; set; }
        public int AddressId { get; set; }
        public int PhoneNumberId { get; set; }
        public string Email { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Password { get; set; }

        public Address Address { get; set; }
        public Passport Passport { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public EmployeePosition Position { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}