using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.DomainEvents
{
    public class DomainEvent : Message, INotification
    {
        protected DomainEvent(Guid aggregateId) : base(aggregateId)
        {
            Timestamp = DateTime.Now;
        }

        protected DomainEvent(Guid aggregateId, string? messageType) : base(aggregateId, messageType)
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}