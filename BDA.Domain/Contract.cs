using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Contract : EntityWithTypeId<int>
    {
        [Display(Name = "И/Н вклада")]
        public int DepositId { get; set; }
        [Display(Name = "И/Н клиента")]
        public int DepositorId { get; set; }
        [Display(Name = "И/Н сотрудника")]
        public int EmployeeId { get; set; }
        [Display(Name = "И/Н счёта")]
        public int AccountId { get; set; }
        [Display(Name = "Дата заключения")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ConclusionDate { get; set; }
        [Display(Name = "Дата расторжения")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? TerminationDate { get; set; }

        public Account Account { get; set; }
        public Deposit Deposit { get; set; }
        public Depositor Depositor { get; set; }
        public Employee Employee { get; set; }
    }
}