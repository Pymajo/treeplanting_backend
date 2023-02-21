using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.Commands
{
    public abstract class Command : Request
    {
        protected Command(Guid aggregateId) : base(aggregateId)
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }
        public ValidationResult? ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
