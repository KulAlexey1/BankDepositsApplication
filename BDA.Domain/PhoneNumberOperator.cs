using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class PhoneNumberOperator : EntityWithTypeId<int>
    {
        public PhoneNumberOperator()
        {
            PhoneNumberOperatorsCodes = new HashSet<PhoneNumberOperatorCode>();
            PhoneNumbers = new HashSet<PhoneNumber>();
        }

        [Display(Name = "Оператор")]
        public string Operator { get; set; }

        public ICollection<PhoneNumberOperatorCode> PhoneNumberOperatorsCodes { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}