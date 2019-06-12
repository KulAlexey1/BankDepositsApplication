using BDA.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BDA.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryable<T> AsQueryable<T>() where T : Entity;

        void Create<T>(T entity) where T : Entity;

        void Update<T>(T entity) where T : Entity;

        void Delete<T>(T entity) where T : Entity;

        Task ExecuteQuery(RawSqlString sql, CancellationToken cancellationToken = default(CancellationToken));

        Task ExecuteQuery(RawSqlString sql, IEnumerable<Object> parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task Commit();

        Task Rollback();
    }
}
