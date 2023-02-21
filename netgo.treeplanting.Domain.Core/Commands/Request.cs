using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.Commands
{
    public  class Request : IRequest
    {
        protected Request(Guid aggregateId)
        {
            AggregateId = aggregateId;
            MessageType = GetType().Name;
        }
        public string MessageType { get; private set; }
        public Guid AggregateId { get; private set; }
    }
}
