using netgo.treeplanting.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Events.User
{
    public class UserDeleteEvent : DomainEvent
    {
        public UserDeleteEvent(Guid aggregateId) : base(aggregateId)
        {

        }
    }
}
