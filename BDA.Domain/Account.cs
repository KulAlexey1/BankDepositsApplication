using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{

    public class Account : EntityWithTypeId<int>
    {
        public Account()
        {
            AccountOperations = new HashSet<AccountOperation>();
        }

        [Display(Name = "Дата открытия")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OpeningDate { get; set; }
        [Display(Name = "Дата закрытия")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ClosingDate { get; set; }
        [Display(Name = "Сумма")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }
        [Display(Name = "Валюта")]
        public string CurrencyName => CurrencyConsts.Dict[Currency];
        public Contract Contract { get; set; }
        public ICollection<AccountOperation> AccountOperations { get; set; }
    }
}