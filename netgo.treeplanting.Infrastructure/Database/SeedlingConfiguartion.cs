using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using netgo.treeplanting.Domain.Entities;

namespace netgo.treeplanting.Infrastructure.Database
{
    public class SeedlingConfiguartion : IEntityTypeConfiguration<Seedling>
    {
        public void Configure(EntityTypeBuilder<Seedling> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TreeSpecies)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.HasOne(seedling => seedling.Treeschool)
                .WithMany(treeschool => treeschool.Seedlings)
                .IsRequired();


            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.XCoordinate)
                .IsRequired();

            builder.Property(x => x.YCoordinate)
                .IsRequired();
        }
    }
}
