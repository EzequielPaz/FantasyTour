using FT.Utilities.Request;
using FT.Domain.Utilities;  
using System.Linq.Expressions;
using Response = FT.Domain.Utilities.Response;

namespace FT.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<Response> ListAsync(
            BasePaginationRequest filters,
            Expression<Func<T, bool>>? extraFilter = null
        );
    }
}
