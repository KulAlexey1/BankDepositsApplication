using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Locality : EntityWithTypeId<int>
    {
        public Locality()
        {
            IssuingAuthoritiesLocalities = new HashSet<IssuingAuthorityLocality>();
            Streets = new HashSet<Street>();
        }

        [Display(Name = "Область")]
        public string Region { get; set; }
        [Display(Name = "Населённый пункт")]
        public string LocalityName { get; set; }

        [Display(Name = "Тип населённого пункта")]
        public LocalityType LocalityType { get; set; }
        [Display(Name = "Тип населённого пункта")]
        public string LocalityTypeName
        {
            get { return LocalityTypeConsts.Dict[this.LocalityType]; }
        }
        public ICollection<IssuingAuthorityLocality> IssuingAuthoritiesLocalities { get; set; }
        public ICollection<Street> Streets { get; set; }
    }
}