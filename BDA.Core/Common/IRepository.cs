using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDA.Core
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : Entity
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task Commit();

        Task Rollback();
    }

    public interface IRepository<T, TId> : IRepository<T>, IReadOnlyRepository<T, TId> where T : EntityWithTypeId<TId>
    {
    }
}
