using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Passport : EntityWithTypeId<int>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public int CitizenshipId { get; set; }
        public string Number { get; set; }
        public string IdentificationNumber { get; set; }
        public int IssuingAuthorityId { get; set; }
        public int IssuingAuthorityLocalityId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Citizenship Citizenship { get; set; }
        public IssuingAuthorityLocality IssuingAuthority { get; set; }
        public Depositor Depositor { get; set; }
        public Employee Employee { get; set; }
    }
}