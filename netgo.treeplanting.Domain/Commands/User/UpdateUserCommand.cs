using netgo.treeplanting.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class UpdateUserCommand : UserCommand
    {
        public UpdateUserCommand(
            string userName,
            string email,
            Guid id) : base(id)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
