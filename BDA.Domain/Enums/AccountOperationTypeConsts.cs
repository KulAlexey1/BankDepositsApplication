using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Domain
{
    public class AccountOperationTypeConsts
    {
        public static readonly IReadOnlyDictionary<AccountOperationType, string> Dict = new Dictionary<AccountOperationType, string>()
        {
            { AccountOperationType.Credit, "Пополнение" },
            { AccountOperationType.Debit, "Списание" }
        };
    }
}
