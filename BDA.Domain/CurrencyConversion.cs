using System;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class CurrencyConversion : Entity
    {
        public Currency Currency { get; set; }
        public bool Buy { get; set; }
        public decimal AmountInNativeCurrencyPerUnit { get; set; }
    }
}