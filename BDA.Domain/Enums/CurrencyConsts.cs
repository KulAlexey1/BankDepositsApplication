using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Domain
{
    public class CurrencyConsts
    {
        public static readonly IReadOnlyDictionary<Currency, string> Dict = new Dictionary<Currency, string>()
        {
            { Currency.BYN, "BYN" },
            { Currency.RUB, "RUB"},
            { Currency.USD, "USD"},
            { Currency.EUR, "EUR"}
        };
    }
}
