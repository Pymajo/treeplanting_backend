using netgo.treeplanting.Domain.Commands.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.User
{
    public class UpdateUserCommandValidation : UserCommandValidation<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            ValidateUserName();
            ValiateEmail();
            ValidateUserId();
        }
    }
}
