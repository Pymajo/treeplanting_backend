using netgo.treeplanting.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.DomainNotifications
{
    public class StoredDomainNotification : DomainNotification
    {
        public Guid Id { get; private set; }
        public string? Data { get; private set; }

        public StoredDomainNotification(
            DomainNotification domainNotification,
            string data) : base(
                domainNotification.AggregateId,
                domainNotification.Key,
                domainNotification.Value,
                domainNotification.Code,
                domainNotification.MessageType)
        {
            Id = Guid.NewGuid();
            Data = data;
        }

        // EF Constructor
        protected StoredDomainNotification() : base(string.Empty, string.Empty)
        { }

    }
}
