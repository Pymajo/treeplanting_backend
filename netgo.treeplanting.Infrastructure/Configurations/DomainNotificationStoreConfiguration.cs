using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using netgo.treeplanting.Domain.Core.DomainEvents;
using netgo.treeplanting.Domain.Core.DomainNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Infrastructure.Configurations
{
    public class DomainNotificationStoreConfiguration : IEntityTypeConfiguration<StoredDomainNotification>
    {
        public void Configure(EntityTypeBuilder<StoredDomainNotification> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Data)
                .HasColumnName("Data")
                .HasColumnType("varchar(350)");

            builder.Property(c => c.AggregateId)
                .HasColumnName("AggregateId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(c => c.MessageType)
                .HasColumnName("MessageType")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Timestamp)
                .HasColumnName("Timestamp")
                .HasColumnType("datetime2(7)")
                .IsRequired();

            builder.Property(c => c.DomainNotificationId)
                .HasColumnName("DomainNotificationId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(c => c.Key)
                .HasColumnName("Key")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Value)
                .HasColumnName("Value")
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Code)
                .HasColumnName("Code")
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Version)
                .HasColumnName("Version")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
