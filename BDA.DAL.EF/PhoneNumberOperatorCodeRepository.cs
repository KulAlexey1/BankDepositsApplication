using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class PhoneNumberOperatorCodeRepository : GenericRepository<PhoneNumberOperatorCode>, IPhoneNumberOperatorCodeRepository
    {
        public PhoneNumberOperatorCodeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
