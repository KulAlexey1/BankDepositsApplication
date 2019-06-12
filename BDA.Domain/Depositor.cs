using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Depositor : EntityWithTypeId<int>
    {
        public Depositor()
        {
            Contracts = new HashSet<Contract>();
        }

        public int PassportId { get; set; }
        public int AddressId { get; set; }
        public int PhoneNumberId { get; set; }
        public string Email { get; set; }

        public Address Address { get; set; }
        public Passport Passport { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}