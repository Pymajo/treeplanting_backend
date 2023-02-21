using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.DomainEvents
{
    public class Message : IRequest
    {
        protected Message(Guid aggregateId)
        {
            AggregateId = aggregateId;
            MessageType = GetType().Name;
        }
        protected Message(Guid aggregateId, string? messageType)
        {
            AggregateId = aggregateId;
            MessageType = messageType;
        }

        public Guid AggregateId { get; private set; }
        public string? MessageType { get; private set; }
    }
}
