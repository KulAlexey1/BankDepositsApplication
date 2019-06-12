using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class PassportRepository : GenericRepository<Passport, int>, IPassportRepository
    {
        public PassportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
