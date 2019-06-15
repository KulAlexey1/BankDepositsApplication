using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class AccountOperation : EntityWithTypeId<int>
    {
        [Display(Name = "И/Н расчётной операции")]
        public int AccountId { get; set; }
        [Display(Name = "Дата и время")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Display(Name = "Сумма")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Тип расчётной операции")]
        public AccountOperationType Type { get; set; }
        [Display(Name = "Тип расчётной операции")]
        public string TypeName => AccountOperationTypeConsts.Dict[Type];
        public Account Account { get; set; }
    }
}