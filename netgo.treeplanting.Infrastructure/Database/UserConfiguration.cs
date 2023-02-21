using Microsoft.AspNetCore.Identity;
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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnType("varchar(25)"); ;

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(125)");

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("varchar(100)"); ;

            builder.Property(x => x.EmailRegistered).IsRequired();

            builder.Property(x => x.IsAdmin).IsRequired();

            builder.Property(x => x.TreecoinsDeterminer).IsRequired();

            builder.Property(x => x.PlantingOfficer).IsRequired();

            builder.Property(x => x.PollManager).IsRequired();

            builder.Property(x => x.SeedlingsManager).IsRequired();

            builder.Property(x => x.Treecoins).IsRequired();
        }
    }
}
