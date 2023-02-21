using netgo.treeplanting.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class CreateUserCommand : UserCommand
    {
        public CreateUserCommand(
            string userName,
            string passwordHash,
            string email,
            bool emailRegistered,
            bool isAdmin,
            bool treecoinsDeterminer,
            bool plantingOfficer,
            bool pollManager,
            bool seedlingsManager,
            int treecoins,
            Guid id) : base(id)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            EmailRegistered = emailRegistered;
            IsAdmin = isAdmin;
            TreecoinsDeterminer = treecoinsDeterminer;
            PlantingOfficer = plantingOfficer;
            PollManager = pollManager;
            SeedlingsManager = seedlingsManager;
            Treecoins = treecoins;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}