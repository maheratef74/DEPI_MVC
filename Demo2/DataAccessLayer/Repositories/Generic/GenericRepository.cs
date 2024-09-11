using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<int> Count()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> Filter)
        {
            return await _dbContext.Set<T>().CountAsync(Filter);
        }

        public void Delete(int id)
        {
            T entity = _dbContext.Set<T>().Find(id);
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        // dbContext.Orders
        //  .Where(order => order.Price > 5000)
        //  .Include(order => order.Products)
        //
        public async Task<IEnumerable<T>> GetWith(string[]? Includes = null, Expression<Func<T, bool>>? Filter = null)
        {
            // "OrderProducts"
            IQueryable<T> query = _dbContext.Set<T>();

            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            if (Includes != null)
            {
                foreach (var item in Includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();
            //return query.ToImmutableList();
        }

        public async Task<IEnumerable<T>> ListAllAsync(Expression<Func<T, object>>? orderBy = null, int page = 1, int pageSize = 5, params Expression<Func<T, object>>[] includes)
        {
            var query = InitializeQuery(includes);

            if (orderBy != null)
            {
                 query = query.OrderBy(orderBy);  // order => order.Price
            }
            if (pageSize > 0)
            {
                query = query.Skip((page-1) * pageSize).Take(pageSize);
            }
            // page = 1-1,  pageSize = 10
            // page = 2-1,  pageSize = 10
            // page = 3-1,  pageSize = 10
            return await query.ToListAsync();
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }

        private IQueryable<T> InitializeQuery(params Expression<Func<T, object>>[] includes)
        {
            // order => order.OrderProducts
            var query = _dbContext.Set<T>().AsQueryable();
            if(includes.Any())
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
