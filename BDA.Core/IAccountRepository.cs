﻿using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Core
{
    public interface IAccountRepository : IRepository<Account, int>
    {
    }
}