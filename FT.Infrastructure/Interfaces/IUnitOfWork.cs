using FT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Tour> Tours { get; }
        IGenericRepository<User> Users { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
