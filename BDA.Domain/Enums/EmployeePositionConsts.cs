using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Domain
{
    public static class EmployeePositionConsts
    {
        public const string Employee = "Employee";
        public const string Cashier = "Cashier";
        public const string Manager = "Manager";
        public const string Admin = "Admin";
        public const string CashierOrManager = "Cashier,Manager";
        public const string CashierOrManagerOrAdmin = "Cashier,Manager,Admin";

        public static readonly IReadOnlyDictionary<EmployeePosition, string> Dict = new Dictionary<EmployeePosition, string>()
        {
            { EmployeePosition.Employee, "Сотрудник" },
            { EmployeePosition.Cashier, "Кассир" },
            { EmployeePosition.Manager, "Менеджер" },
            { EmployeePosition.Admin, "Администратор" }            
        };
    }
}
