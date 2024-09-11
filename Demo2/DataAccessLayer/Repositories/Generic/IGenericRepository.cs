using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetById(int id);
        Task<T> Add(T entity);
        T Update(T entity);
        void Delete(int id);
        Task<int> Count();
        Task<IEnumerable<T>> GetWith(string[]? Includes = null, Expression<Func<T, bool>>? Filter = null);
        Task<int> CountWhere(Expression<Func<T, bool>> Filter);
        Task<IEnumerable<T>> ListAllAsync(Expression<Func<T, object>>? orderBy = null, int page = 1, int pageSize = 5, params Expression<Func<T, object>>[] includes);

        // product => product.Price > 15000
    }
}
