using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Errors;

namespace netgo.treeplanting.Api.Controllers
{
    public class TreePlantingController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        protected TreePlantingController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected async Task AddIdentityErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                await NotifyError(result.ToString(), error.Description);
            }
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected async Task NotifyError(string code, string message)
        {
            await _mediator.RaiseEventAsync(new DomainNotification(code, message));
        }

        protected async Task NotifyModelStateErrors()
        {
            IEnumerable<ModelError> erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (ModelError erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                await NotifyError(string.Empty, erroMsg);
            }
        }

        protected new IActionResult Response(object? result = null)
        {
            if (IsValidOperation())
            {
                return Ok(
                    new
                    {
                        success = true,
                        data = result
                    });
            }

            if (_notifications.GetNotifications().Any(n => n.Code == DomainErrorCodes.InsufficientPermissions))
            {
                return Forbid();
            }

            if (_notifications.GetNotifications().Any(n => n.Code == DomainErrorCodes.ObjectDoesNotExist))
            {
                return NotFound(
                    new
                    {
                        success = false,
                        errors = _notifications.GetNotifications().Select(n => n.Value)
                    });
            }

            return BadRequest(
                new
                {
                    success = false,
                    errors = _notifications.GetNotifications().Select(n => n.Value)
                });
        }


    }
}
