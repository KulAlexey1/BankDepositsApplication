using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Street : EntityWithTypeId<int>
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
        }

        [Display(Name = "И/Н населённого пункта")]
        public int LocalityId { get; set; }
        [Display(Name = "Улица")]
        public string StreetName { get; set; }
        [Display(Name = "Почтовый индекс")]
        [DataType(DataType.PostalCode)]
        public int? Postcode { get; set; }

        [Display(Name = "Населённый пункт")]
        public virtual Locality Locality { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}