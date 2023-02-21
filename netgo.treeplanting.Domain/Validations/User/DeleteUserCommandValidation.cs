using netgo.treeplanting.Domain.Commands.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.User
{
    public class DeleteUserCommandValidation : UserCommandValidation<DeleteUserCommand>
    {
        public DeleteUserCommandValidation()
        {
            ValidateUserId();
        }
    }
}
