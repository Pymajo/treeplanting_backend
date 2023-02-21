using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.DomainEvents
{
    public class StoredDomainEvent : DomainEvent
    {
        public StoredDomainEvent(DomainEvent domainEvent, string data) : base(
            domainEvent.AggregateId,
            domainEvent.MessageType)
        {
            Id = Guid.NewGuid();
            Data = data;
        }

        protected StoredDomainEvent() : base(Guid.NewGuid())
        { }

        public string? Data { get; private set; }
        public Guid Id { get; private set; }
    }
}
