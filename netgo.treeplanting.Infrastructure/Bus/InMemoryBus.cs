using MediatR;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Commands;
using netgo.treeplanting.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IDomainEventStore _domainEventStore;
        private readonly IMediator _mediator;

        public InMemoryBus(IDomainEventStore domainEventStore, IMediator mediator)
        {
            _domainEventStore = domainEventStore;
            _mediator = mediator;
        }

        public Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query)
        {
            return _mediator.Send(query);
        }

        public async Task RaiseEventAsync<T>(T @event) where T : DomainEvent
        {
            await _domainEventStore.SaveAsnyc(@event);

            await _mediator.Publish(@event);
        }

        public async Task SendCommandAsync<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

    }
}
