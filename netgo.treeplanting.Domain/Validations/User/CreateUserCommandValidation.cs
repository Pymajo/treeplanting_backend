using FluentValidation;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Errors;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.User
{
    public class CreateUserCommandValidation : UserCommandValidation<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            ValidateUserId();
            ValidatePasswordHash();
            ValidateUserId();
            ValidateUserName();
        }
    }
}
