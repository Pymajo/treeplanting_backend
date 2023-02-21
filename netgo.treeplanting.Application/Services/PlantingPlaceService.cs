using AutoMapper;
using Microsoft.Extensions.Configuration;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Application.ViewModels;
using netgo.treeplanting.Domain.Commands.PlantingPlace;
using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Entities;
using netgo.treeplanting.Domain.Errors;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.Services
{
    public class PlantingPlaceService: IPlantingPlaceService
    {

        private readonly IMediatorHandler _bus;
        private readonly IPlantingPlaceSystemRepository _plantingPlaceRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public PlantingPlaceService(IMediatorHandler bus, IPlantingPlaceSystemRepository plantingPlaceRepository, IMapper mapper, IConfiguration config)
        {
            _bus = bus;
            _plantingPlaceRepository = plantingPlaceRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<List<PlantingPlaceViewModel>> GetAllPlantingPlacesAsync()
        {
            var plantingPlaces = await _plantingPlaceRepository.GetAll().ToListAsync();
            var plantingPlaceList = plantingPlaces.Select(x => PlantingPlaceViewModel.FromPlantingPlace(x)).ToList();
            return plantingPlaceList;
        }

        public async Task<PlantingPlaceViewModel?> CreatePlantingPlaceAsync(PlantingPlace plantingPlace)
        {
            var plantingPlaceDto = await _plantingPlaceRepository.GetAll().FirstOrDefaultAsync(x => x.Id == plantingPlace.Id);

            if (plantingPlaceDto == null || plantingPlaceDto.Id != plantingPlaceDto.Id)
            {

                var cmd = new CreatePlantingPlaceCommand(
                    Guid.NewGuid(),
                    plantingPlace.XCoordinate,
                    plantingPlace.YCoordinate,
                    plantingPlace.Image,
                    plantingPlace.Description,
                    plantingPlace.SeedlingId,
                    plantingPlace.PlantingAreaId);

                await _bus.SendCommandAsync(cmd);

                var response = new PlantingPlaceViewModel
                {
                    Id = cmd.Id,
                    XCoordinate = cmd.XCoordinate,
                    YCoordinate = cmd.YCoordinate,
                    Image = cmd.Image,
                    Description = cmd.Description,
                    SeedlingId = cmd.SeedlingId,
                    PlantingAreaId = cmd.PlantingAreaId
                };

                return response;
            }
            else
            {
                await _bus.RaiseEventAsync(new DomainNotification("Create",
                "PlantingPlace existiert bereits, Bitte erneut versuchen.",
                DomainErrorCodes.ObjectAllreadyExist));
                return null;
            }
        }

        public async Task<PlantingPlaceViewModel> EditPlantingPlaceAsync(PlantingPlace plantingPlace)
        {
            var cmd = new EditPlantingPlaceCommand(
                    plantingPlace.Id,
                    plantingPlace.XCoordinate,
                    plantingPlace.YCoordinate,
                    plantingPlace.Image,
                    plantingPlace.Description,
                    plantingPlace.SeedlingId,
                    plantingPlace.PlantingAreaId);

            await _bus.SendCommandAsync(cmd);

            var response = new PlantingPlaceViewModel
            {
                Id = cmd.Id,
                XCoordinate = cmd.XCoordinate,
                YCoordinate = cmd.YCoordinate,
                Image = cmd.Image,
                Description = cmd.Description,
                SeedlingId = cmd.SeedlingId,
                PlantingAreaId = cmd.PlantingAreaId
            };

            return response;
        }
    }
}
