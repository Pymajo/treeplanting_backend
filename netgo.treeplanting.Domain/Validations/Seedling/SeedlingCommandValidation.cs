using FluentValidation;
using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.Seedling
{
    public class SeedlingCommandValidation<T> : AbstractValidator<T> where T : SeedlingCommand
    {
        public SeedlingCommandValidation()
        {
            ValidateSeedlingId();
        }
        public void ValidateSeedlingId()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.SeedlingInvalidId)
                .WithMessage("Id may not be empty or null");
        }
    }
}
