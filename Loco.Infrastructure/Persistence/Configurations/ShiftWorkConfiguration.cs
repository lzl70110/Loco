using Loco.Domain.Work;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Loco.GCommon.EntityValidationConstants.ShiftWork;

namespace Loco.Infrastructure.Persistence.Configurations
{
    public sealed class ShiftWorkConfiguration : IEntityTypeConfiguration<ShiftWork>
    {
        public void Configure(EntityTypeBuilder<ShiftWork> b)
        {
            b.HasKey(x => x.Id);

            // Relationship: ShiftWork -> Locomotive (FK LocoId)
            b.HasOne(x => x.Locomotive)
                .WithMany(l => l.ShiftWorks)
                .HasForeignKey(x => x.LocoId)
                .OnDelete(DeleteBehavior.Restrict);

            // DateOnly -> date
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
                dt => DateOnly.FromDateTime(DateTime.SpecifyKind(dt, DateTimeKind.Utc)));

            b.Property(x => x.Date)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            // CreatedOn: same converter, same SQL type
            b.Property(x => x.CreatedOn)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            // Decimal columns
            b.Property(x => x.InitialValue).HasColumnType(Dec);
            b.Property(x => x.FinalValue).HasColumnType(Dec);
            b.Property(x => x.Amount).HasColumnType(Dec);

            // String lengths
            b.Property(x => x.CreatedBy).HasMaxLength(CreatedByMaxLength);
            b.Property(x => x.Note).HasMaxLength(NoteMaxLength);

             
            b.HasIndex(x => new { x.LocoId, x.Date, x.Shift })
             .IsUnique()
             .HasDatabaseName("IX_ShiftWork_Loco_Date_Shift");
            }
    }
}