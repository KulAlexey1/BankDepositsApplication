using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Depositor : EntityWithTypeId<int>
    {
        public Depositor()
        {
            Contracts = new HashSet<Contract>();
        }

        [Display(Name = "И/Н паспорта")]
        public int PassportId { get; set; }
        [Display(Name = "И/Н адреса")]
        public int AddressId { get; set; }
        [Display(Name = "И/Н телефонного номера")]
        public int PhoneNumberId { get; set; }
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Адрес")]
        public Address Address { get; set; }
        [Display(Name = "Паспорт")]
        public Passport Passport { get; set; }
        [Display(Name = "Телефонный номер")]
        public PhoneNumber PhoneNumber { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}