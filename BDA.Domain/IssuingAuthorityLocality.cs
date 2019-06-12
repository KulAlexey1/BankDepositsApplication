using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class IssuingAuthorityLocality : Entity
    {
        public IssuingAuthorityLocality()
        {
            Passports = new HashSet<Passport>();
        }

        public int IssuingAuthorityId { get; set; }
        public int LocalityId { get; set; }

        public IssuingAuthority IssuingAuthority { get; set; }
        public Locality Locality { get; set; }
        public ICollection<Passport> Passports { get; set; }
    }
}