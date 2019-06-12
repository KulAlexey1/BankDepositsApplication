using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Core
{
    public interface IAccountOperationRepository : IRepository<AccountOperation, int>
    {
    }
}