using netgo.treeplanting.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class EmailRegisterCommand : UserCommand
    {

        public EmailRegisterCommand(
            bool emailRegistered,
            Guid id) : base(id)
        {
            Id = id;
            EmailRegistered = emailRegistered;
        }

        public override bool IsValid()
        {
            ValidationResult = new EmailRegisterCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
