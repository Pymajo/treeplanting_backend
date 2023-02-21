using netgo.treeplanting.Domain.Commands.User;

namespace netgo.treeplanting.Domain.Validations.User
{
    public class EmailRegisterCommandValidation : UserCommandValidation<EmailRegisterCommand>
    {
        public EmailRegisterCommandValidation()
        {
            ValidateEmailRegistered();
            ValidateUserId();
        }

    }
}
