using FluentValidation;
using netgo.treeplanting.Domain.Commands.PlantingPlace;
using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.PlantingPlace
{
    public class PlantingPlaceCommandValidation<T> : AbstractValidator<T> where T : PlantingPlaceCommand
    {
        public PlantingPlaceCommandValidation()
        {
            ValidatePlantingPlaceId();
        }
        public void ValidatePlantingPlaceId()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.PlantingPlaceInvalidId)
                .WithMessage("Id may not be empty or null");
        }
    }
}
