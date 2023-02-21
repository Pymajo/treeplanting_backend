using FluentValidation;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.User
{
    public class UserCommandValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        public UserCommandValidation()
        {
            ValidateUserId();
        }

        protected void ValidateUserId()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.UserInvalidId)
                .WithMessage("Id may not be empty or null");
        }

        protected void ValidateEmailRegistered()
        {
            RuleFor(c => c.EmailRegistered)
                .Equal(true)
                .WithMessage("Email may not be Registered")
                .WithErrorCode(DomainErrorCodes.UserInvalidEmailRegistered)
                .NotEmpty()
                .NotNull()
                .WithMessage("EmailRegistered may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidEmailRegistered);                
        }

        protected void ValiateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidEmail)
                .MaximumLength(125)
                .WithMessage("Email field cannot be set to more than 125 characters")
                .WithErrorCode(DomainErrorCodes.UserInvalidEmail);
        }

        protected void ValidateUserName()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserName may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidUserName)
                .MaximumLength(25)
                .WithMessage("UserName field cannot be set to more than 125 characters")
                .WithErrorCode(DomainErrorCodes.UserInvalidUserName);
        }

        protected void ValidatePasswordHash()
        {
            RuleFor(c => c.PasswordHash)
                .NotEmpty()
                .NotNull()
                .WithMessage("PasswordHash may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidPasswordHash)
                .Length(8, 100)
                .WithMessage("The PasswordHash field cannot be set to more than 100 characters and no less than 8")
                .WithErrorCode(DomainErrorCodes.UserInvalidPasswordHash);
        }

        protected void ValidateUserIsAdmin()
        {
            RuleFor(c => c.IsAdmin)
                .NotEmpty()
                .NotNull()
                .WithMessage("IsAdmin may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidIsAdmin);

        }

        protected void ValidateDemoteAdmin()
        {
            RuleFor(c => c.IsAdmin)
                .NotEmpty()
                .NotNull()
                .WithMessage("IsAdmin may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidIsAdmin)
                .Equal(false)
                .WithMessage("User is not an Admin")
                .WithErrorCode(DomainErrorCodes.InvalidDemoteAdmin);
        }
        
        protected void ValidatePromoteAdmin()
        {
            RuleFor(c => c.IsAdmin)
                .NotEmpty()
                .NotNull()
                .WithMessage("IsAdmin may not be empty or null")
                .WithErrorCode(DomainErrorCodes.UserInvalidIsAdmin)
                .Equal(true)
                .WithMessage("User is already an Admin")
                .WithErrorCode(DomainErrorCodes.InvalidPromoteAdmin);
        }
    }
}
