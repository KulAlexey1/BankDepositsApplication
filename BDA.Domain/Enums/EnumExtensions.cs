using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Domain
{
    public static class EnumExtensions
    {
        public static string ConvertToString(this LocalityType localityType)
        {
            return LocalityTypeConsts.Dict[localityType];
        }

        public static string ConvertToString(this AccountOperationType accountOperationType)
        {
            return AccountOperationTypeConsts.Dict[accountOperationType];
        }

        public static string ConvertToString(this Citizenship citizenship)
        {
            return CitizenshipConsts.Dict[citizenship];
        }

        public static string ConvertToString(this EmployeePosition employeePosition)
        {
            return EmployeePositionConsts.Dict[employeePosition];
        }

        public static string ConvertToString(this Currency currency)
        {
            return CurrencyConsts.Dict[currency];
        }
    }
}
