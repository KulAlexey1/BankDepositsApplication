using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Domain
{
    public class EntityWithTypeId<T> : Entity
    {
        public T Id { get; set; }
    }
}
