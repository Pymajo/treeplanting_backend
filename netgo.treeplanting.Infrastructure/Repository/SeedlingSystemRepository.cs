using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using netgo.treeplanting.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Repository
{
    public class SeedlingSystemRepository : SystemRepository<Seedling>, ISeedlingSystemRepository
    {
        private ApplicationDbContext _context;

        public SeedlingSystemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Seedling> GetSeedlingAsync(Guid id)
        {

            Seedling? seedling = await _context.Seedling.FindAsync(id);
            return seedling;
        }
    }
}
