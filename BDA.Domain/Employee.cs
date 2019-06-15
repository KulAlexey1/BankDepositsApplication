using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Employee : EntityWithTypeId<int>
    {
        public Employee()
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
        [Display(Name = "Начальная дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Конечная дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Адрес")]
        public Address Address { get; set; }
        [Display(Name = "Паспорт")]
        public Passport Passport { get; set; }
        [Display(Name = "Телефонный номер")]
        public PhoneNumber PhoneNumber { get; set; }
        [Display(Name = "Должность")]
        public EmployeePosition Position { get; set; }
        [Display(Name = "Должность")]
        public string PositionName => EmployeePositionConsts.Dict[Position];

        public ICollection<Contract> Contracts { get; set; }
    }
}