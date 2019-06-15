using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Address : EntityWithTypeId<int>
    {
        public Address()
        {
            Depositors = new HashSet<Depositor>();
            Employees = new HashSet<Employee>();
        }

        [Display(Name = "И/Н улицы")]
        public int StreetId { get; set; }
        [Display(Name = "Дом")]
        public int House { get; set; }
        [Display(Name = "Корпус")]
        public string Housing { get; set; }
        [Display(Name = "Квартира")]
        public int Apartment { get; set; }

        public Street Street { get; set; }
        public ICollection<Depositor> Depositors { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}