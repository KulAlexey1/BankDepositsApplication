using BDA.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BDA.Core
{
    public class GenericReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Entity
    {
        protected readonly IUnitOfWork unitOfWork;

        public GenericReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes)
        {
            var entities = unitOfWork.AsQueryable<T>();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }

            if (includes != null)
            {
                entities = includes.Aggregate(entities, (current, include) => current.Include(include));
            }

            return entities;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GenericReadOnlyRepository() {
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
