using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.ViewModels
{
    public class MeViewModel
    {
        public MeViewModel(Guid id, string userName, string email, bool isAdmin,bool treecoinsDeterminer, bool plantingOfficer, bool pollManager, bool seedlingsManager, int treecoins)
        {
            Id = id;
            UserName = userName;
            Email = email;
            IsAdmin = isAdmin;
            TreecoinsDeterminer = treecoinsDeterminer;
            PlantingOfficer = plantingOfficer;
            PollManager = pollManager;
            SeedlingsManager = seedlingsManager;
            Treecoins = treecoins;
        }

        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? TreecoinsDeterminer { get; set; }
        public bool? PlantingOfficer { get; set; }
        public bool? PollManager { get; set; }
        public bool? SeedlingsManager { get; set; }
        public int? Treecoins { get; set; }
    }
}
