using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Interfaces.Repositories.System
{
    public interface IPlantingPlaceSystemRepository :IRepository<PlantingPlace>
    {
        Task<PlantingPlace> GetPlantingPlaceAsync(Guid id);
    }
}
