using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BDA.Core
{
    public interface IReadOnlyRepository<T> : IDisposable where T : Entity
    {
        IQueryable<T> GetBy(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes);
    }

    public interface IReadOnlyRepository<T, TId> : IReadOnlyRepository<T> where T : EntityWithTypeId<TId>
    {
        Task<T> GetById(TId id, params Expression<Func<T, object>>[] includes);
    }
}