using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class PhoneNumberOperatorCode : Entity
    {
        [Display(Name = "И/Н оператора")]
        public int PhoneNumberOperatorId { get; set; }
        [Display(Name = "Код")]
        public int Code { get; set; }

        [Display(Name = "Оператор")]
        public virtual PhoneNumberOperator PhoneNumberOperator { get; set; }
    }
}