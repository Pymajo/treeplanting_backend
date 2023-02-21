using MediatR;
using Microsoft.Extensions.Logging;
using netgo.treeplanting.Domain.Commands.PlantingPlace;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Events.PlantingPlace;
using netgo.treeplanting.Domain.Events.User;
using netgo.treeplanting.Domain.Interfaces;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.CommandHandler
{
    public class PlantingPlaceCommandHandler : CommandHandler,
        IRequestHandler<CreatePlantingPlaceCommand>,
        IRequestHandler<EditPlantingPlaceCommand>
    {
        private readonly ILogger<PlantingPlaceCommandHandler> _logger;
        private readonly IPlantingPlaceSystemRepository _plantingPlaceSystemRepository;
        private readonly IMediatorHandler _bus;

        public PlantingPlaceCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            ILogger<PlantingPlaceCommandHandler> logger,
            IPlantingPlaceSystemRepository plantingPlaceSystemRepository,
            INotificationHandler<DomainNotification> notifcations) : base(uow, bus, notifcations)
        {
            _logger = logger;
            _plantingPlaceSystemRepository = plantingPlaceSystemRepository;
            _bus = bus;
        }

        public async Task<Unit> Handle(CreatePlantingPlaceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }
            var plantingPlace = new PlantingPlace(
                request.Id,
                request.XCoordinate,
                request.YCoordinate,
                request.Image,
                request.Description,
                request.SeedlingId,
                request.PlantingAreaId);

            await _plantingPlaceSystemRepository.AddAsync(plantingPlace);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(PlantingPlaceCreateEvent.FromCommand(request));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(EditPlantingPlaceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var plantingPlace = await _plantingPlaceSystemRepository.GetPlantingPlaceAsync(request.Id);

            plantingPlace.XCoordinate = request.XCoordinate;
            plantingPlace.YCoordinate = request.YCoordinate;
            plantingPlace.Image = request.Image;
            plantingPlace.Description = request.Description;
            plantingPlace.SeedlingId = request.SeedlingId;
            plantingPlace.PlantingAreaId = request.PlantingAreaId;

            _plantingPlaceSystemRepository.Update(plantingPlace);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new PlantingPlaceSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }
    }
}
