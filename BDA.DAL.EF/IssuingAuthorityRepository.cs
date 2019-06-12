using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class IssuingAuthorityRepository : GenericRepository<IssuingAuthority, int>, IIssuingAuthorityRepository
    {
        public IssuingAuthorityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
