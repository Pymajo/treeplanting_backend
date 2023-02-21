using netgo.treeplanting.Domain.Core.DomainEvents;
using netgo.treeplanting.Domain.Core.DomainNotifications;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Infrastructure.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.EventSourcing
{
    public class DomainEventStore : IDomainEventStore
    {
        private readonly EventStoreDbContext _eventStoreDbContext;
        private readonly DomainNotificationStoreDbContext _domainNotificationStoreDbContext;

        public DomainEventStore(
            EventStoreDbContext eventStoreDbContext,
            DomainNotificationStoreDbContext domainNotificationStoreDbContext)
        {
            _eventStoreDbContext = eventStoreDbContext;
            _domainNotificationStoreDbContext = domainNotificationStoreDbContext;
        }

        public async Task SaveAsnyc<T>(T domainEvent) where T : DomainEvent
        {
            var serializedData = JsonConvert.SerializeObject(domainEvent);

            switch (domainEvent)
            {
                case DomainNotification d:
                    var storedDomainNotification = new StoredDomainNotification(d, serializedData);
                    _domainNotificationStoreDbContext.StoredDomainNotifications.Add(storedDomainNotification);
                    await _domainNotificationStoreDbContext.SaveChangesAsync();
                    break;

                default:
                    var storedDomainEvent = new StoredDomainEvent(
                        domainEvent,
                        serializedData);
                    _eventStoreDbContext.StoredDomainEvents.Add(storedDomainEvent);
                    await _eventStoreDbContext.SaveChangesAsync();
                    break;
            }
        }
    }
}
