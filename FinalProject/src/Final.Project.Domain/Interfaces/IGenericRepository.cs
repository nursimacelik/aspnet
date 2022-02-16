using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Final.Project.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
