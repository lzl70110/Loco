using Loco.Domain.Fuel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Loco.GCommon.EntityValidationConstants.Fuel;

namespace Loco.Infrastructure.Persistence.Configurations
{
    public sealed class FuelConfiguration : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> b)
        {
            // Primary key
            b.HasKey(x => x.Id);

            // Relationship: Fuel -> Locomotive (FK LocoId)
            b.HasOne(x => x.Locomotive)
                .WithMany()  
                .HasForeignKey(x => x.LocoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Value converter for DateOnly <-> DateTime (store as date in DB)
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                dt => DateOnly.FromDateTime(DateTime.SpecifyKind(dt, DateTimeKind.Utc)));

            // Fuel date
            b.Property(x => x.Date)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            // CreatedOn ( DateOnly)
            b.Property(x => x.CreatedOn)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            // Decimal columns
            b.Property(x => x.InitialFuel).HasColumnType(Dec);
            b.Property(x => x.FinalFuel).HasColumnType(Dec);
            b.Property(x => x.Refueled).HasColumnType(Dec);
            b.Property(x => x.Consumption).HasColumnType(Dec);

            // CreatedBy + Note constraints
            b.Property(x => x.CreatedBy).HasMaxLength(CreatedByMaxLength);
            b.Property(x => x.Note).HasMaxLength(NoteMaxLength);
        }
    }
}