using BDA.Core;
using BDA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDA.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> AsQueryable<T>() where T : Entity
        {
            return dbContext.Set<T>();
        }

        public void Create<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : Entity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Remove(entity);
        }

        public Task ExecuteQuery(RawSqlString sql, CancellationToken cancellationToken = default(CancellationToken))
        {
            return dbContext.Database.ExecuteSqlCommandAsync(sql, cancellationToken);
        }

        public Task ExecuteQuery(RawSqlString sql, IEnumerable<Object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return dbContext.Database.ExecuteSqlCommandAsync(sql, parameters, cancellationToken);
        }

        public async Task Commit()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    await dbContext.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                }
            }
        }

        public Task Rollback()
        {
            var rollBackTasks = dbContext.ChangeTracker.Entries().Select(async entry =>
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                    default:
                        break;
                }
            });

            return Task.WhenAll(rollBackTasks);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    try
                    {
                        Commit().Wait();
                    }
                    catch
                    {
                        Rollback().Wait();
                    }
                    dbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}