using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.ViewModels
{
    public class PlantingPlaceViewModel
    {
        public Guid Id { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid SeedlingId { get; set; }
        public Guid PlantingAreaId { get; set; }


        public static PlantingPlaceViewModel FromPlantingPlace(PlantingPlace plantingPlace)
        {
            return new PlantingPlaceViewModel()
            {
                Id = plantingPlace.Id,
                XCoordinate = plantingPlace.XCoordinate,
                YCoordinate = plantingPlace.YCoordinate,
                Image = plantingPlace.Image,
                Description = plantingPlace.Description,
                SeedlingId = plantingPlace.SeedlingId,
                PlantingAreaId = plantingPlace.PlantingAreaId
            };
        }
    }
}
