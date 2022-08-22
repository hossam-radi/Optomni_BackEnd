using Microsoft.EntityFrameworkCore;
using Optmni.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Optmni.DAL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private OptmniDbContext _context;

        public BaseRepository(OptmniDbContext context)
        {
            _context = context;
        }
        public T Add(T item)
        {
            _context.Set<T>().Add(item);
            return item;
        }

        public async Task<T> AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            return item;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> items)
        {
            _context.Set<T>().AddRange(items);
            return items;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> items)
        {
            await _context.Set<T>().AddRangeAsync(items);
            return items;
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Count(match);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().CountAsync(match);
        }

        public void Delete(Guid id)
        {
            var dataSet = this.GetById(id);
            _context.Set<T>().Remove(dataSet);
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> match, Expression<Func<T, object>>[] includes)
        {
            var dataSets = _context.Set<T>().Where(match);
            foreach (var item in includes)
            {
                dataSets.Include(item);
            }
            return dataSets.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Update(T item)
        {
            _context.Set<T>().Update(item);
            return item;
        }
    }
}
