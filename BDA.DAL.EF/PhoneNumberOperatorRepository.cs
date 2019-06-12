using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class PhoneNumberOperatorRepository : GenericRepository<PhoneNumberOperator, int>, IPhoneNumberOperatorRepository
    {
        public PhoneNumberOperatorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
