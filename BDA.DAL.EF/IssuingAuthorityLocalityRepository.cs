using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class IssuingAuthorityLocalityRepository : GenericRepository<IssuingAuthorityLocality>, IIssuingAuthorityLocalityRepository
    {
        public IssuingAuthorityLocalityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
