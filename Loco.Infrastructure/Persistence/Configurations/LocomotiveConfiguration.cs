using Loco.Domain.Locomotives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Loco.GCommon.EntityValidationConstants.Locomotive;

namespace Loco.Infrastructure.Persistence.Configurations
{
    public sealed class LocomotiveConfiguration : IEntityTypeConfiguration<Locomotive>
    {
        public void Configure(EntityTypeBuilder<Locomotive> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(LocomotiveNumberLength);

            b.HasIndex(x => x.Number).IsUnique();

            b.Property(x => x.CreatedBy)
                .HasMaxLength(CreatedByMaxLength);

            b.Property(x => x.Note)
                .HasMaxLength(NoteMaxLength);

            // DateOnly -> date
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                dt => DateOnly.FromDateTime(DateTime.SpecifyKind(dt, DateTimeKind.Utc)));

            b.Property(x => x.CreatedOn)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            // Navigation: Locomotive -> Fuel
            b.HasMany(l => l.Fuels)
                .WithOne(f => f.Locomotive)
                .HasForeignKey(f => f.LocoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Navigation: Locomotive -> ShiftWork
            b.HasMany(l => l.ShiftWorks)
                .WithOne(sw => sw.Locomotive)
                .HasForeignKey(sw => sw.LocoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}