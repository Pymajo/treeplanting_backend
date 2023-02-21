using netgo.treeplanting.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Entities
{
    public class PlantingPlace : Entity
    {
        public PlantingPlace(
           Guid id,
            int xCoordinate,
            int yCoordinate,
            string image,
            string description,
            Guid seedlingId,
            Guid plantingAreaId) : base(id)

        {
            Id = id;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Image = image;
            Description = description;
            SeedlingId = seedlingId;
            PlantingAreaId = plantingAreaId;
        }

        public Guid Id { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid SeedlingId { get; set; }
        public virtual Seedling? Seedling { get; set; }
        public Guid PlantingAreaId { get; set; }
        public virtual PlantingArea? PlantingArea { get; set; }

    }
}
