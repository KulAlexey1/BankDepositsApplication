using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class ContractRepository : GenericRepository<Contract, int>, IContractRepository
    {
        public ContractRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}