using netgo.treeplanting.Domain.Commands.PlantingPlace;
using netgo.treeplanting.Domain.Commands.Seedling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.PlantingPlace
{
    public class EditPlantingPlaceCommandValidation: PlantingPlaceCommandValidation<EditPlantingPlaceCommand>
    {
        public EditPlantingPlaceCommandValidation()
        {
            ValidatePlantingPlaceId();
        }
    }
}
