using FT.Domain.Entities;
using FT.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Infrastructure.Repositories
{
    public class UserRepository: GenericRepository<User>
    {
        public UserRepository(ApplicationDbContext context): base(context)
        {
        }
    }
}
