using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Locality : EntityWithTypeId<int>
    {
        public Locality()
        {
            IssuingAuthoritiesLocalities = new HashSet<IssuingAuthorityLocality>();
            Streets = new HashSet<Street>();
        }

        public string Region { get; set; }
        public string LocalityName { get; set; }

        public LocalityType LocalityType { get; set; }
        public ICollection<IssuingAuthorityLocality> IssuingAuthoritiesLocalities { get; set; }
        public ICollection<Street> Streets { get; set; }
    }
}