using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class LocalityRepository : GenericRepository<Locality, int>, ILocalityRepository
    {
        public LocalityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
