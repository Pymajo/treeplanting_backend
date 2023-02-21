using netgo.treeplanting.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class DemoteTreecoinsDeterminerCommand: UserCommand
    {
        public DemoteTreecoinsDeterminerCommand(Guid id) : base(id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DemoteTreecoinsDeterminerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
