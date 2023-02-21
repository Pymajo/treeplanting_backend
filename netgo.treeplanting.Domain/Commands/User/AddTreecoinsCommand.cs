using MediatR;
using netgo.treeplanting.Domain.Validations.User;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class AddTreecoinsCommand : UserCommand
    {
        public int Deposit { get; set; }

        public AddTreecoinsCommand(Guid id, int deposit): base(id)
        {
            Id = id;
            Deposit = deposit;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddTreecoinsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
