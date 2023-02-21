using netgo.treeplanting.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.Seedling
{
    public abstract class SeedlingCommand : Command
    {
        public Guid Id { get; set; }
        public string TreeSpecies { get; set; }
        public Guid TreeschoolId { get; set; }
        public int Price { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        protected SeedlingCommand(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}
