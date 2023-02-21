using MediatR;
using Microsoft.Extensions.Logging;
using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Events.Seedling;
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
    public class SeedlingCommandHandler : CommandHandler,
        IRequestHandler<CreateSeedlingCommand>,
        IRequestHandler<EditSeedlingCommand>
    {
        private readonly ILogger<SeedlingCommandHandler> _logger;
        private readonly ISeedlingSystemRepository _seedlingSystemRepository;
        private readonly IMediatorHandler _bus;

        public SeedlingCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler bus,
            ILogger<SeedlingCommandHandler> logger,
            ISeedlingSystemRepository seedlingSystemRepository,
            INotificationHandler<DomainNotification> notifcations) : base(uow, bus, notifcations)
        {
            _logger = logger;
            _seedlingSystemRepository = seedlingSystemRepository;
            _bus = bus;
        }

        public async Task<Unit> Handle(CreateSeedlingCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }
            var seedling = new Seedling(
                request.Id,
                request.TreeSpecies,
                request.TreeschoolId,
                request.Price,
                request.XCoordinate,
                request.YCoordinate);

            await _seedlingSystemRepository.AddAsync(seedling);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(SeedlingCreateEvent.FromCommand(request));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(EditSeedlingCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }
            
            var seedling = await _seedlingSystemRepository.GetSeedlingAsync(request.Id);
            
            seedling.TreeSpecies = request.TreeSpecies;
            seedling.TreeschoolId = request.TreeschoolId;
            seedling.Price = request.Price;
            seedling.XCoordinate = request.XCoordinate;
            seedling.YCoordinate = request.YCoordinate;

            _seedlingSystemRepository.Update(seedling);

            if(await Commit())
            {
                await _bus.RaiseEventAsync(new SeedlingSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }
    }
}
