using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Core.DomainEvents;

namespace netgo.treeplanting.Domain.Events.Seedling
{
    public class SeedlingCreateEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string TreeSpecies { get; set; }
        public Guid TreeschoolId { get; set; }
        public int Price { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public SeedlingCreateEvent(
            Guid id,
            string treeSpecies,
            Guid treeschoolId,
            int price,
            int xCoordinate,
            int yCoordinate,
            Guid aggregateId) : base(aggregateId)
        {
            Id = id;
            TreeSpecies = treeSpecies;
            TreeschoolId = treeschoolId;
            Price = price;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }


        public static SeedlingCreateEvent FromCommand(CreateSeedlingCommand command)
        {
            return new SeedlingCreateEvent(
                command.Id,
                command.TreeSpecies,
                command.TreeschoolId,
                command.Price,
                command.XCoordinate,
                command.YCoordinate,
                command.AggregateId);
        }
    }

}

