﻿using netgo.treeplanting.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Commands.User
{
    public class DemotePollManagerCommand: UserCommand
    {
        public DemotePollManagerCommand(Guid id): base(id)
        {
            Id = id; 
        }

        public override bool IsValid()
        {
            ValidationResult = new DemotePollManagerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

