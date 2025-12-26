using FT.Domain.Entities;
using FT.Infrastructure.Context;
using FT.Infrastructure.Interfaces;

namespace FT.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Tour> Tours { get; }
        public IGenericRepository<User> Users { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Tours = new GenericRepository<Tour>(_context);
            Users = new GenericRepository<User>(_context);

        }

        public int SaveChanges() => _context.SaveChanges();
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
