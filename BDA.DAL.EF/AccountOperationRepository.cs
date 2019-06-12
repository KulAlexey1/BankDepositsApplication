using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class AccountOperationRepository : GenericRepository<AccountOperation, int>, IAccountOperationRepository
    {
        public AccountOperationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
