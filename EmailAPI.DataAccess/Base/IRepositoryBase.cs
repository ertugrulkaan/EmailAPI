using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailAPI.DataAccess.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll();
        T Get(long id);
        IEnumerable<T> GetBy(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
