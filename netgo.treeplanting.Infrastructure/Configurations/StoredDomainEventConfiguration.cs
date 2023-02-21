 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using netgo.treeplanting.Domain.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Configurations
{
    public class StoredDomainEventConfiguration : IEntityTypeConfiguration<StoredDomainEvent>
    {
        public void Configure(EntityTypeBuilder<StoredDomainEvent> builder)
        {
            builder.Property(c => c.Timestamp)
                .HasColumnName("CreationDate");


            builder.Property(c => c.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");
        }
    }
}
