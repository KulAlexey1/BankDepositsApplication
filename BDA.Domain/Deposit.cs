using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Deposit : EntityWithTypeId<int>
    {
        public Deposit()
        {
            Contracts = new HashSet<Contract>();
        }

        [Display(Name = "Вклад")]
        public string DepositName { get; set; }
        [Display(Name = "Платёжный период")]
        public int PaymentPeriod { get; set; }
        [Display(Name = "И/Н срока вклада")]
        public int DepositTermId { get; set; }
        [Display(Name = "Ставка")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }
        [Display(Name = "Возможность пополнения")]
        public bool AccountReplenishment { get; set; }

        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }
        [Display(Name = "Валюта")]
        public string CurrencyName => CurrencyConsts.Dict[Currency];
        [Display(Name = "Срок вклада")]
        public DepositTerm DepositTerm { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}