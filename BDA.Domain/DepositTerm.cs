using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class DepositTerm : EntityWithTypeId<int>
    {
        [Display(Name = "Количество дней")]
        public int DaysAmount { get; set; }
        [Display(Name = "Количество месяцев")]
        public int MonthsAmount { get; set; }
        [Display(Name = "Количество лет")]
        public int YearsAmount { get; set; }

        public ICollection<Deposit> Deposits { get; set; }
    }
}