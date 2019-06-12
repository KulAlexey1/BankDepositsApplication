using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class StreetRepository : GenericRepository<Street, int>, IStreetRepository
    {
        public StreetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
