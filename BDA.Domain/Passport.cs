using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDA.Domain
{
    public class Passport : EntityWithTypeId<int>
    {
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "День рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Пол")]
        public bool Gender { get; set; }
        [Display(Name = "Номер паспорта")]
        public string Number { get; set; }
        [Display(Name = "Идентификационный номер")]
        public string IdentificationNumber { get; set; }
        [Display(Name = "И/Н выдавшего органа")]
        public int IssuingAuthorityId { get; set; }
        [Display(Name = "И/Н населённого пункта")]
        public int IssuingAuthorityLocalityId { get; set; }
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime IssueDate { get; set; }
        [Display(Name = "Дата истечения срока действия")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Гражданство")]
        public Citizenship CitizenshipId { get; set; }
        [Display(Name = "Гражданство")]
        public string CitizenshipName => CitizenshipConsts.Dict[CitizenshipId];
        public IssuingAuthorityLocality IssuingAuthorityLocality { get; set; }
        [Display(Name = "Клиент")]
        public Depositor Depositor { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }

        [Display(Name = "ФИО")]
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
        [Display(Name = "ФИО")]
        public string ShortFullName => $"{LastName} {FirstName[0]}. {MiddleName[0]}.";
    }
}