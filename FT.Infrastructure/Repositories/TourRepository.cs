using FT.Domain.Entities;
using FT.Infrastructure.Context;

namespace FT.Infrastructure.Repositories
{
    public class TourRepository: GenericRepository<Tour>
    {
        public TourRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
