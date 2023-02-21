using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.DomainEvents;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Errors;
using netgo.treeplanting.Domain.Events.User;
using netgo.treeplanting.Domain.Interfaces;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.CommandHandler
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<UpdateUserCommand>,
        IRequestHandler<CreateUserCommand>,
        IRequestHandler<DeleteUserCommand>,
        IRequestHandler<EmailRegisterCommand>,
        IRequestHandler<PromoteAdminCommand>,
        IRequestHandler<DemoteAdminCommand>,
        IRequestHandler<PromoteTreecoinsDeterminerCommand>,
        IRequestHandler<DemoteTreecoinsDeterminerCommand>,
        IRequestHandler<PromotePlantingOfficerCommand>,
        IRequestHandler<DemotePlantingOfficerCommand>,
        IRequestHandler<PromotePollManagerCommand>,
        IRequestHandler<DemotePollManagerCommand>,
        IRequestHandler<PromoteSeedlingsManagerCommand>,
        IRequestHandler<DemoteSeedlingsManagerCommand>,
        IRequestHandler<AddTreecoinsCommand>,
        IRequestHandler<RemoveTreecoinsCommand>
    {
        private readonly ILogger<UserCommandHandler> _logger;
        private readonly IUserSystemRepository _userSystemRepository;
        private readonly IMediatorHandler _bus;

        public UserCommandHandler(
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            ILogger<UserCommandHandler> logger,
            IUserSystemRepository userSystemRepository,
            IMediatorHandler bus) : base(uow, bus, notifications)
        {
            _logger = logger;
            _userSystemRepository = userSystemRepository;
            _bus = bus;
        }

        public async Task<Unit> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = new User(
               request.UserName,
               request.PasswordHash,
               request.Email,
               request.EmailRegistered,
               request.IsAdmin,
               request.TreecoinsDeterminer,
               request.PlantingOfficer,
               request.PollManager,
               request.SeedlingsManager,
               request.Treecoins,
               request.Id);

            await _userSystemRepository.AddAsync(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(UserCreatedEvent.FromCommand(request));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            user.Email = request.Email;
            user.PasswordHash = request.PasswordHash;
            user.UserName = request.UserName;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }
        public async Task<Unit> Handle(
            EmailRegisterCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            user.EmailRegistered = true;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
            PromoteAdminCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (user.IsAdmin)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist bereits Admin",
                        DomainErrorCodes.UserIsAlreadyAdmin));

                return Unit.Value;
            }

            user.IsAdmin = true;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;

        }

        public async Task<Unit> Handle(
            DemoteAdminCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (!user.IsAdmin)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist kein Admin",
                        DomainErrorCodes.UserIsNotAdmin));

                return Unit.Value;
            }

            user.IsAdmin = false;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
            PromoteTreecoinsDeterminerCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (user.TreecoinsDeterminer)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist bereits TreecoinsDeterminer",
                        DomainErrorCodes.UserIsAlreadyTreecoinsDeterminer));

                return Unit.Value;
            }

            user.TreecoinsDeterminer = true;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
           DemoteTreecoinsDeterminerCommand request,
           CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (!user.TreecoinsDeterminer)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist kein Treecoins Determiner",
                        DomainErrorCodes.UserIsNotTreecoinsDeterminer));

                return Unit.Value;
            }

            user.TreecoinsDeterminer = false;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
            PromotePlantingOfficerCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (user.PlantingOfficer)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist bereits Planting Officer",
                        DomainErrorCodes.UserIsAlreadyPlantingOfficer));

                return Unit.Value;
            }

            user.PlantingOfficer = true;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
           DemotePlantingOfficerCommand request,
           CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (!user.PlantingOfficer)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist kein Planting Officer",
                        DomainErrorCodes.UserIsNotPlantingOfficer));

                return Unit.Value;
            }

            user.PlantingOfficer = false;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
            PromotePollManagerCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (user.PollManager)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist bereits Poll Manager",
                        DomainErrorCodes.UserIsAlreadyPollManager));

                return Unit.Value;
            }

            user.PollManager = true;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
           DemotePollManagerCommand request,
           CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (!user.PollManager)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist kein Poll Manager",
                        DomainErrorCodes.UserIsNotPollManager));

                return Unit.Value;
            }

            user.PollManager = false;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }



        public async Task<Unit> Handle(
            PromoteSeedlingsManagerCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (user.SeedlingsManager)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist bereits Seedlings Manager",
                        DomainErrorCodes.UserIsAlreadySeedlingsManager));

                return Unit.Value;
            }

            user.SeedlingsManager = true;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
           DemoteSeedlingsManagerCommand request,
           CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            if (!user.SeedlingsManager)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        request.MessageType,
                        "Der Benutzer ist kein Seedlings Manager",
                        DomainErrorCodes.UserIsNotSeedlingsManager));

                return Unit.Value;
            }

            user.SeedlingsManager = false;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(
            DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);



            await _userSystemRepository.RemoveAsync(user.Id);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserDeleteEvent(request.AggregateId));
            }

            return Unit.Value;
        }
        public async Task<Unit> Handle(
            AddTreecoinsCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value; ;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            user.Treecoins += request.Deposit;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
                request.Treecoins = user.Treecoins;
            }

            return Unit.Value; ;
        }

        public async Task<Unit> Handle(RemoveTreecoinsCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return Unit.Value; ;
            }

            var user = await _userSystemRepository.GetByIdAsync(request.Id);

            user.Treecoins -= request.Withdraw;

            _userSystemRepository.Update(user);

            if (await Commit())
            {
                await _bus.RaiseEventAsync(new UserSavedEvent(request.AggregateId));
                request.Treecoins = user.Treecoins;
            }

            return Unit.Value; ;
        }
    }
}
