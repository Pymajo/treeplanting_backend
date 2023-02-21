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
    public class PlantingPlaceSystemRepository : SystemRepository<PlantingPlace>, IPlantingPlaceSystemRepository
    {
        private ApplicationDbContext _context;

        public PlantingPlaceSystemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PlantingPlace> GetPlantingPlaceAsync(Guid id)
        {

            PlantingPlace? plantingPlace = await _context.PlantingPlace.FindAsync(id);
            return plantingPlace;
        }
    }
}
