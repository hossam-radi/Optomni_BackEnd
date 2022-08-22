using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Optmni.DAL.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        // get 
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        IEnumerable<T> Get(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] includes);

        // Count 
        int Count();
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> match);
        int Count(Expression<Func<T, bool>> match);
        // add
        T Add(T item);
        Task<T> AddAsync(T item);
        IEnumerable<T> AddRange(IEnumerable<T> items);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> items);
        // update
        T Update(T item);
        // delete 
        void Delete(Guid id);
        void Delete(T item);

    }
}
