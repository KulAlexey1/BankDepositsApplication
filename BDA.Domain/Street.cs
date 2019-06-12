using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class Street : EntityWithTypeId<int>
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
        }

        public int LocalityId { get; set; }
        public string StreetName { get; set; }
        public int? Postcode { get; set; }

        public virtual Locality Locality { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}