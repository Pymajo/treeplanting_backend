using netgo.treeplanting.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Entities
{
    public class PlantingArea : Entity
    {
        public PlantingArea(
            Guid id,
            int xCentralCoordinate,
            int yCentralCoordinate,
            string image,
            string description) : base(id)
        {
            Id = id;
            XCentralCoordinate = xCentralCoordinate;
            YCentralCoordinate = yCentralCoordinate;
            Image = image;
            Description = description;
        }

        public Guid Id { get; set; }
        public int XCentralCoordinate { get; set; }
        public int YCentralCoordinate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
