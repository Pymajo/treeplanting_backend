using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.ViewModels
{
    public class SeedlingViewModel
    {
        public Guid Id { get; set; }
        public string TreeSpecies { get; set; } = null!;
        public Guid TreeschoolId { get; set; }
        public int Price { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public static SeedlingViewModel FromSeedling(Seedling seedling)
        {
            return new SeedlingViewModel()
            {
                Id = seedling.Id,
                TreeSpecies = seedling.TreeSpecies,
                TreeschoolId = seedling.TreeschoolId,
                Price = seedling.Price,
                XCoordinate = seedling.XCoordinate,
                YCoordinate = seedling.YCoordinate,
            };
        }
    }
}
