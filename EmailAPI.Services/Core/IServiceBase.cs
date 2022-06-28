using EmailAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmailAPI.Services.Core
{
    public interface IServiceBase<T> 
    {
        IQueryable<T> GetQuery();
        List<T> GetAll();
        T GetById(int id);
        List<T> GetBy(Expression<Func<T, bool>> expression);
        ServiceResult Create(T entity);
        ServiceResult Update(T entity);
        ServiceResult Delete(T entity);
    }
}
