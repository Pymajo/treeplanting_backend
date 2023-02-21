using MediatR;
using Microsoft.AspNetCore.Mvc;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Application.Services;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;

namespace netgo.treeplanting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedlingController : TreePlantingController
    {
        private readonly ISeedlingService _seedlingService;
        private readonly ISeedlingSystemRepository _seedlingRepository;

        public SeedlingController(
            INotificationHandler<DomainNotification> notifications,
            ISeedlingSystemRepository seedlingRepository,
            IMediatorHandler mediator,
            ISeedlingService seedlingService) : base(notifications, mediator)
        {
            _seedlingService = seedlingService;
            _seedlingRepository = seedlingRepository;
        }

        [HttpGet("seedlings")]
        public async Task<IActionResult> GetAsync()
        {
            var config = await _seedlingService.GetAllSeedlingsAsync();

            return Response(config);
        }

        [HttpPost("seedlings")]
        public async Task<IActionResult> CreateSeedlingAsync(Seedling seedling)
        {
            var config = await _seedlingService.CreateSeedlingAsync(seedling);

            return Response(config);
        }

        [HttpPut("seedlings")]
        public async Task<IActionResult> EditSeedlingAsync(Seedling seedling)
        {
            var config = await _seedlingService.EditSeedlingAsync(seedling);

            return Response(config);
        }
    }
}
