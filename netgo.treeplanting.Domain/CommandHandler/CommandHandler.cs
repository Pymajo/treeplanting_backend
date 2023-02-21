using MediatR;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Commands;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Errors;
using netgo.treeplanting.Domain.Interfaces;

namespace netgo.treeplanting.Domain.CommandHandler
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUnitOfWork _uow;

        public CommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifcations)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifcations;
            _bus = bus;
        }

        public async Task<bool> Commit()
        {
            if (_notifications.HasNotifications())
            {
                return false;
            }

            if (await _uow.CommitAsync())
            {
                return true;
            }

            await _bus.RaiseEventAsync(new DomainNotification("Commit",
                "Problem beim Speichern der Daten. Bitte erneut versuchen.",
                DomainErrorCodes.CommitError));
            return false;
        }

        protected async Task NotifyValidationErrors(Command message)
        {
            if (message.ValidationResult == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(NotifyValidationErrors)} should only be called if message.ValidationResult is not null");
            }

            foreach (var error in message.ValidationResult.Errors)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(message.MessageType, error.ErrorMessage, error.ErrorCode));
            }
        }
    }
}
