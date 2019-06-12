using BDA.Core;
using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.DAL.EF
{
    public class AddressRepository : GenericRepository<Address, int>, IAddressRepository
    {
        public AddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
