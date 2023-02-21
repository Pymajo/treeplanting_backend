using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.DomainEvents
{
    public interface IDomainEventStore
    {
        Task SaveAsnyc<T>(T domainEvent) where T : DomainEvent;
    }
}
