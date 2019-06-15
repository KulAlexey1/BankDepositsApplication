using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class IssuingAuthority : EntityWithTypeId<int>
    {
        public IssuingAuthority()
        {
            IssuingAuthoritiesLocalities = new HashSet<IssuingAuthorityLocality>();
        }

        [Display(Name = "Выдавший орган")]
        public string Name { get; set; }

        public ICollection<IssuingAuthorityLocality> IssuingAuthoritiesLocalities { get; set; }
    }
}