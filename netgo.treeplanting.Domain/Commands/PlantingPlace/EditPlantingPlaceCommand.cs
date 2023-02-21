using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Validations.PlantingPlace;
using netgo.treeplanting.Domain.Validations.Seedling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.PlantingPlace
{
    public class EditPlantingPlaceCommand : PlantingPlaceCommand
    {
        public EditPlantingPlaceCommand(
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

        public override bool IsValid()
        {
            ValidationResult = new EditPlantingPlaceCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
