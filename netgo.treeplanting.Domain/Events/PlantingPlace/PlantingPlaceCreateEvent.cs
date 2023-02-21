using netgo.treeplanting.Domain.Commands.PlantingPlace;
using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Core.DomainEvents;

namespace netgo.treeplanting.Domain.Events.PlantingPlace
{
    public class PlantingPlaceCreateEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid SeedlingId { get; set; }
        public Guid PlantingAreaId { get; set; }

        public PlantingPlaceCreateEvent(
            Guid id,
            int xCoordinate,
            int yCoordinate,
            string image,
            string description,
            Guid seedlingId,
            Guid plantingAreaId,
            Guid aggregateId) : base(aggregateId)
        {
            Id = id;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Image = image;
            Description = description;
            SeedlingId = seedlingId;
            PlantingAreaId = plantingAreaId;
        }


        public static PlantingPlaceCreateEvent FromCommand(CreatePlantingPlaceCommand command)
        {
            return new PlantingPlaceCreateEvent(
                command.Id,
                command.XCoordinate,
                command.YCoordinate,
                command.Image,
                command.Description,
                command.SeedlingId,
                command.PlantingAreaId,
                command.AggregateId);
        }
    }

}

