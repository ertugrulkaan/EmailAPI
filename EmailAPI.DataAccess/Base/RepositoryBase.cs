using EmailAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmailAPI.DataAccess.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
        protected readonly ModelContext context;

        public RepositoryBase(ModelContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> GetQuery()
        {
            return context.Set<T>().AsNoTracking();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking()
                                   .AsEnumerable<T>();
        }

        public virtual T Get(long id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression).AsNoTracking();
        }

        public virtual void Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
    }
}
