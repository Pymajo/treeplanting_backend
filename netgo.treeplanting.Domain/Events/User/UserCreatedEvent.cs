using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Events.User
{
    public class UserCreatedEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool EmailRegistered { get; set; }
        public bool IsAdmin { get; set; }
        public bool TreecoinsDeterminer {get; set; }
        public bool PlantingOfficer {get; set; }
        public bool PollManager { get; set; }
        public bool SeedlingsManager { get; set; }
        public int Treecoins { get; set; }

        public UserCreatedEvent(
            Guid id,
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
            Guid aggregateId) : base(aggregateId)
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

        public static UserCreatedEvent FromCommand(CreateUserCommand command)
        {
            return new UserCreatedEvent(
                command.Id,
                command.UserName,
                command.PasswordHash,
                command.Email,
                command.EmailRegistered,
                command.IsAdmin,
                command.TreecoinsDeterminer,
                command.PlantingOfficer,
                command.PollManager,
                command.SeedlingsManager,
                command.Treecoins,
                command.AggregateId);
        }
    }
}
