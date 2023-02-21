using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using netgo.treeplanting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Database
{
    public class PlantingPlaceConfiguration
    {
        public void Configure(EntityTypeBuilder<PlantingPlace> builder)
        {
            builder.HasKey(x => x.Id);

           

            builder.Property(x => x.XCoordinate)
                    .IsRequired();

            builder.Property(x => x.YCoordinate)
                    .IsRequired();

            builder.HasOne(plantingPlace => plantingPlace.Seedling).WithOne(seedling => seedling.PlantingPlace)
                   .IsRequired();
        }
    }
}
