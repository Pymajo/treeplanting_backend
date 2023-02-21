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
    public class PlantingPlaceController : TreePlantingController
    {
        private readonly IPlantingPlaceService _plantingPlaceService;
        private readonly IPlantingPlaceSystemRepository _plantingPlaceRepository;

        public PlantingPlaceController(
            INotificationHandler<DomainNotification> notifications,
            IPlantingPlaceSystemRepository plantingPlaceRepository,
            IMediatorHandler mediator,
            IPlantingPlaceService plantingPlaceService) : base(notifications, mediator)
        {
            _plantingPlaceService = plantingPlaceService;
            _plantingPlaceRepository = plantingPlaceRepository;
        }

        [HttpGet("plantingPlace")]
        public async Task<IActionResult> GetAsync()
        {
            var config = await _plantingPlaceService.GetAllPlantingPlacesAsync();

            return Response(config);
        }

        [HttpPost("plantingPlace")]
        public async Task<IActionResult> CreatePlantingPlaceAsync(PlantingPlace plantingPlace)
        {
            var config = await _plantingPlaceService.CreatePlantingPlaceAsync(plantingPlace);

            return Response(config);
        }

        [HttpPut("plantingPlace")]
        public async Task<IActionResult> EditPlantingPlaceAsync(PlantingPlace plantingPlace)
        {
            var config = await _plantingPlaceService.EditPlantingPlaceAsync(plantingPlace);

            return Response(config);
        }
    }
}
