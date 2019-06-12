using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Core
{
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
    }
}
