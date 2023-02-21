using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using netgo.treeplanting.Infrastructure.Database;

namespace netgo.treeplanting.Infrastructure.Repository
{
    public class UserSystemRepository : SystemRepository<User>, IUserSystemRepository
    {
        private ApplicationDbContext _context;

        public UserSystemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(Guid id)
        {

            User? user = await _context.User.FindAsync(id);
            return user;
        }
    }
}
