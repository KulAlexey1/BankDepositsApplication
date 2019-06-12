using BDA.Core;
using BDA.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class CurrencyConversionRepository : GenericRepository<CurrencyConversion>, ICurrencyConversionRepository
    {
        public CurrencyConversionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CurrencyConversion GetByCurrency(Currency currency)
        {
            var currencyConversion = unitOfWork
                .AsQueryable<CurrencyConversion>()
                .FirstOrDefault(cc => cc.Currency == currency);

            return currencyConversion;
        }
    }
}
