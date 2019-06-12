using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class DepositorTermRepository : GenericRepository<DepositTerm, int>, IDepositorTermRepository
    {
        public DepositorTermRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
