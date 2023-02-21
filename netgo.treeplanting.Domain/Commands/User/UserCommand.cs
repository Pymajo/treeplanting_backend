﻿using netgo.treeplanting.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public abstract class UserCommand : Command
    {
        public Guid Id { get; protected set; }
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

        protected UserCommand(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}
