using BDA.Core;
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
    public class GenericRepository<T> : GenericReadOnlyRepository<T>, IRepository<T> where T : Entity
    {
        public GenericRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual void Create(T entity)
        {
            unitOfWork.Create(entity);
        }

        public virtual void Delete(T entity)
        {
            unitOfWork.Delete(entity);
        }

        public virtual void Update(T entity)
        {
            unitOfWork.Update(entity);
        }

        public virtual Task Commit()
        {
            return unitOfWork.Commit();
        }

        public virtual Task Rollback()
        {
            return unitOfWork.Rollback();
        }
    }

    public class GenericRepository<T, TId> : GenericRepository<T>, IRepository<T, TId> where T : EntityWithTypeId<TId>
    {
        public GenericRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual Task<T> GetById(TId id, params Expression<Func<T, object>>[] includes)
        {
            var entities = unitOfWork.AsQueryable<T>();
            
            if (includes != null)
            {
                entities = includes.Aggregate(entities, (current, include) => current.Include(include));
            }

            return entities.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
    }
}