using netgo.treeplanting.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class RemoveTreecoinsCommand : UserCommand
    {
        public int Withdraw { get; set; }

        public RemoveTreecoinsCommand(Guid id, int withdraw) : base(id)
        {
            Id = id;
            Withdraw = withdraw;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveTreecoinsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
