using MediatR;
using netgo.treeplanting.Domain.Core.Commands;
using netgo.treeplanting.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task RaiseEventAsync<T>(T @event) where T : DomainEvent;

        Task SendCommandAsync<T>(T command) where T : Command;

        Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query);
    }
}

