using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class PhoneNumberOperator : EntityWithTypeId<int>
    {
        public PhoneNumberOperator()
        {
            PhoneNumberOperatorsCodes = new HashSet<PhoneNumberOperatorCode>();
            PhoneNumbers = new HashSet<PhoneNumber>();
        }

        public string Operator { get; set; }

        public ICollection<PhoneNumberOperatorCode> PhoneNumberOperatorsCodes { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}