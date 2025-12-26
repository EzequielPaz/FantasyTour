using FT.Domain.Utilities;
using FT.Infrastructure.Context;
using FT.Infrastructure.Helpers;
using FT.Infrastructure.Interfaces;
using FT.Utilities.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FT.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;


        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);

        // 🔹 Método para verificar si existe algún registro que cumpla una condición
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<Response> ListAsync(
    BasePaginationRequest filters,
    Expression<Func<T, bool>>? extraFilter = null)
        {
            try
            {
                // Validaciones de paginación
                if (filters.PageIndex <= 0) filters.PageIndex = 1;
                if (filters.PageSize <= 0) filters.PageSize = 10;
                if (filters.PageSize > 50) filters.PageSize = 50;

                // Filtro adicional o filtro vacío
                var predicate = extraFilter ?? (_ => true);

                // Consulta base
                var query = _dbSet.AsNoTracking().Where(predicate);

                var totalRecords = await query.CountAsync();

                // Orden dinámico
                if (!string.IsNullOrEmpty(filters.Sort))
                {
                    query = filters.Order == "asc"
                        ? query.OrderByDynamic(filters.Sort, ascending: true)
                        : query.OrderByDynamic(filters.Sort, ascending: false);
                }

                var items = await query
                    .Skip((filters.PageIndex - 1) * filters.PageSize)
                    .Take(filters.PageSize)
                    .ToListAsync();

                var result = new
                {
                    TotalRecords = totalRecords,
                    Records = items,
                    PageIndex = filters.PageIndex,
                    PageSize = filters.PageSize
                };

                return Response.Success(result, "Datos obtenidos correctamente");
            }
            catch (Exception ex)
            {
                return Response.Error("Error al obtener los datos: " + ex.Message, 500);
            }
        }

    }
}
