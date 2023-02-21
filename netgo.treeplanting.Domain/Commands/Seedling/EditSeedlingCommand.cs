using netgo.treeplanting.Domain.Validations.Seedling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.Seedling
{
    public class EditSeedlingCommand : SeedlingCommand
    {
        public EditSeedlingCommand(
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

        public override bool IsValid()
        {
            ValidationResult = new EditSeedlingCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
