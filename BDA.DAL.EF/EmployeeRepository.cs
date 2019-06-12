using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class EmployeeRepository : GenericRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
