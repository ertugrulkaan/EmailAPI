using EmailAPI.Core.Model;
using EmailAPI.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmailAPI.Services.Core
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected IRepositoryBase<T> repository;

        public virtual IQueryable<T> GetQuery()
        {
            return repository.GetQuery();
        }

        public virtual List<T> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public virtual T GetById(int id)
        {
            return repository.Get(id);
        }

        public virtual List<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return repository.GetBy(expression).ToList();
        }

        public virtual ServiceResult Create(T entity)
        {
            repository.Create(entity);

            return ServiceResult.Success;
        }

        public virtual ServiceResult Update(T entity)
        {
            repository.Update(entity);

            return ServiceResult.Success;
        }

        public virtual ServiceResult Delete(T entity)
        {
            repository.Delete(entity);

            return ServiceResult.Success;
        }
    }
}
