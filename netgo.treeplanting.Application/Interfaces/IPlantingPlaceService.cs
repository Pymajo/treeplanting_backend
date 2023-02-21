using netgo.treeplanting.Application.ViewModels;
using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.Interfaces
{
    public interface IPlantingPlaceService
    {
        Task<List<PlantingPlaceViewModel>> GetAllPlantingPlacesAsync();
        Task<PlantingPlaceViewModel> CreatePlantingPlaceAsync(PlantingPlace plantingPlace);
        Task<PlantingPlaceViewModel> EditPlantingPlaceAsync(PlantingPlace plantingPlace);
    }
}
