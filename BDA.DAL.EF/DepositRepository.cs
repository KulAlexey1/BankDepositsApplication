using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class DepositRepository : GenericRepository<Deposit, int>, IDepositRepository
    {
        public DepositRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
