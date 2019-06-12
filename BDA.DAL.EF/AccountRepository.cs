using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class AccountRepository : GenericRepository<Account, int>, IAccountRepository
    {
        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}