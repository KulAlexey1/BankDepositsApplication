using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class IssuingAuthorityLocality : Entity
    {
        public IssuingAuthorityLocality()
        {
            Passports = new HashSet<Passport>();
        }

        [Display(Name = "И/Н выдавшего органа")]
        public int IssuingAuthorityId { get; set; }
        [Display(Name = "И/Н населённого пункта")]
        public int LocalityId { get; set; }

        [Display(Name = "Выдавший орган")]
        public IssuingAuthority IssuingAuthority { get; set; }
        [Display(Name = "Населённый пункт")]
        public Locality Locality { get; set; }
        public ICollection<Passport> Passports { get; set; }
    }
}