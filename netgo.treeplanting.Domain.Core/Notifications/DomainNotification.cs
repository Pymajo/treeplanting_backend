using netgo.treeplanting.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.Notifications
{
    public class DomainNotification : DomainEvent
    {
        public Guid DomainNotificationId { get; private set; }
        public string? Key { get; private set; }
        public string? Value { get; private set; }
        public string? Code { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(Guid id, string? key, string? value, string? code, string? messageType) : base(id, messageType)
        {
            Initialize(key, value, code);
        }

        public DomainNotification(string? key, string? value, string? code) : base(Guid.NewGuid())
        {
            Initialize(key, value, code);
        }

        public DomainNotification(string? key, string? value) : base(Guid.NewGuid())
        {
            Initialize(key, value, string.Empty);
        }

        private void Initialize(string? key, string? value, string? code)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
            Code = code;
        }
    }
}
