using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class CurrencyConversion : Entity
    {
        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }
        [Display(Name = "Валюта")]
        public string CurrencyName => CurrencyConsts.Dict[Currency];
        [Display(Name = "Покупка")]
        public bool Buy { get; set; }
        [Display(Name = "Сумма в национальной валюте на единицу")]
        [DataType(DataType.Currency)]
        public decimal AmountInNativeCurrencyPerUnit { get; set; }
    }
}