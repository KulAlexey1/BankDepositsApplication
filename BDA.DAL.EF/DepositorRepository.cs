using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class DepositorRepository : GenericRepository<Depositor, int>, IDepositorRepository
    {
        public DepositorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
