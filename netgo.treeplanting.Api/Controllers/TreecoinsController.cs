using MediatR;
using Microsoft.AspNetCore.Mvc;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;

namespace netgo.treeplanting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreecoinsController : TreePlantingController
    {
        private readonly IUserService _userService;
        private readonly IUserSystemRepository _userRepository;

        public TreecoinsController(
            INotificationHandler<DomainNotification> notifications,
            IUserSystemRepository userRepository,
            IMediatorHandler mediator,
            IUserService userService) : base(notifications, mediator)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPatch("AddTreecoins/{id}/{deposit}")]
        public async Task<IActionResult> AddTreecoins(Guid id, int deposit)
        {
            var config = await _userService.AddTreecoins(id, deposit);

            return Response(config);
        }


        [HttpPatch("RemoveTreecoins/{id}/{withdraw}")]
        public async Task<IActionResult> RemoveTreecoins(Guid id, int withdraw)
        {
            var config = await _userService.RemoveTreecoins(id, withdraw);

            return Response(config);
        }

    }
}
