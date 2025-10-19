using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Configurations;

public class DomainEventConfiguration : IEntityTypeConfiguration<DomainEvent>
{
    public void Configure(EntityTypeBuilder<DomainEvent> builder)
    {
        builder.ToTable("DomainEvents");

        builder.HasKey(de => de.Id);

        builder.Property(de => de.EventType)
            .IsRequired();

        builder.Property(de => de.OccurredOn)
            .IsRequired();

        builder.Property(de => de.Description)
            .IsRequired();

        builder.HasOne(de => de.Property)
            .WithMany(p => p.DomainEvents)
            .HasForeignKey(de => de.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
