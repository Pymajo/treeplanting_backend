using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Application.ViewModels;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Errors;
using netgo.treeplanting.Domain.Commands.Seedling;

namespace netgo.treeplanting.Application.Services
{
    public class SeedlingService : ISeedlingService
    {
        private readonly IMediatorHandler _bus;
        private readonly ISeedlingSystemRepository _seedlingRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public SeedlingService(IMediatorHandler bus, ISeedlingSystemRepository seedlingRepository, IMapper mapper, IConfiguration config)
        {
            _bus = bus;
            _seedlingRepository = seedlingRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<List<SeedlingViewModel>> GetAllSeedlingsAsync()
        {
            var seedlings =  await _seedlingRepository.GetAll().ToListAsync();
            var seedlingList = seedlings.Select(x => SeedlingViewModel.FromSeedling(x)).ToList(); 
            return seedlingList;
        }

        public async Task<SeedlingViewModel?> CreateSeedlingAsync(Seedling seedling)
        {
            var seedlingDto = await _seedlingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == seedling.Id);

            if (seedlingDto== null || seedlingDto.Id != seedling.Id)
            {

                var cmd = new CreateSeedlingCommand(
                    Guid.NewGuid(),
                    seedling.TreeSpecies,
                    seedling.TreeschoolId,
                    seedling.Price,
                    seedling.XCoordinate,
                    seedling.YCoordinate);

                await _bus.SendCommandAsync(cmd);

                var response = new SeedlingViewModel
                {
                    Id = cmd.Id,
                    TreeSpecies = cmd.TreeSpecies,
                    TreeschoolId = cmd.TreeschoolId,
                    Price = cmd.Price,
                    XCoordinate = cmd.XCoordinate,
                    YCoordinate = cmd.YCoordinate
                };

                return response;
            }
            else
            {
                await _bus.RaiseEventAsync(new DomainNotification("Create",
                "Seedling existiert bereits, Bitte erneut versuchen.",
                DomainErrorCodes.ObjectAllreadyExist));
                return null;
            }
        }

        public async Task<SeedlingViewModel> EditSeedlingAsync(Seedling seedling)
        {
            var cmd = new EditSeedlingCommand(
                    seedling.Id,
                    seedling.TreeSpecies,
                    seedling.TreeschoolId,
                    seedling.Price,
                    seedling.XCoordinate,
                    seedling.YCoordinate);

            await _bus.SendCommandAsync(cmd);

            var response = new SeedlingViewModel
            {
                Id = cmd.Id,
                TreeSpecies = cmd.TreeSpecies,
                TreeschoolId = cmd.TreeschoolId,
                Price = cmd.Price,
                XCoordinate = cmd.XCoordinate,
                YCoordinate = cmd.YCoordinate
            };

            return response;
        }
    }
}
