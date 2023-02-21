using netgo.treeplanting.Domain.Core.Entities;
namespace netgo.treeplanting.Domain.Entities
{
    public class User : Entity
    {
        public User(
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

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool EmailRegistered { get; set; }
        public bool IsAdmin { get; set; } 
        public bool TreecoinsDeterminer { get; set; }
        public bool PlantingOfficer { get; set; }
        public bool PollManager { get; set; }
        public bool SeedlingsManager { get; set; }
        public int Treecoins { get; set; }
    }
}
