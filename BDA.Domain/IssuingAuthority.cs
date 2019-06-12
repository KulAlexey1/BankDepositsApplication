using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class IssuingAuthority : EntityWithTypeId<int>
    {
        public IssuingAuthority()
        {
            IssuingAuthoritiesLocalities = new HashSet<IssuingAuthorityLocality>();
        }

        public string Name { get; set; }

        public ICollection<IssuingAuthorityLocality> IssuingAuthoritiesLocalities { get; set; }
    }
}