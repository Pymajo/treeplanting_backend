using netgo.treeplanting.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Entities
{
    public class Seedling : Entity
    {
        public Seedling(
            Guid id,
            string treeSpecies,
            Guid treeschoolId,
            int price,
            int xCoordinate,
            int yCoordinate) : base(id)
        {
            Id = id;
            TreeSpecies = treeSpecies;
            TreeschoolId = treeschoolId;
            Price = price;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public Guid Id { get; set; }
        public string TreeSpecies { get; set; }
        public Guid TreeschoolId { get; set; }
        public virtual Treeschool? Treeschool { get; set; }
        public int Price { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public virtual PlantingPlace PlantingPlace { get; set; }
    }
}
