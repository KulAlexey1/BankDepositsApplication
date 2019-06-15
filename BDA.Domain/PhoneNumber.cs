using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class PhoneNumber : EntityWithTypeId<int>
    {
        public PhoneNumber()
        {
            Depositors = new HashSet<Depositor>();
            Employees = new HashSet<Employee>();
        }

        [Display(Name = "И/Н оператора")]
        public int OperatorId { get; set; }
        [Display(Name = "Код оператора")]
        public int OperatorCode { get; set; }
        [Display(Name = "Номер")]
        public int Number { get; set; }

        [Display(Name = "Оператор")]
        public PhoneNumberOperator Operator { get; set; }
        public ICollection<Depositor> Depositors { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}