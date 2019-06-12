using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class PhoneNumberOperatorCode : Entity
    {
        public int PhoneNumberOperatorId { get; set; }
        public int Code { get; set; }

        public virtual PhoneNumberOperator PhoneNumberOperator { get; set; }
    }
}