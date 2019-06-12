using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumber, int>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
