using netgo.treeplanting.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.PlantingPlace
{
    public abstract class PlantingPlaceCommand : Command
    {
        public Guid Id { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid SeedlingId { get; set; }
        public Guid PlantingAreaId { get; set; }

        protected PlantingPlaceCommand(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}
