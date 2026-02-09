using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loco.Domain.Locomotives;
using Loco.GCommon.Enums;
using static Loco.GCommon.EntityValidationConstants.ShiftWork;

namespace Loco.Domain.Work
{
    public class ShiftWork
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Locomotive))]
        public int LocoId { get; set; }

        // Navigation MUST have set;
        public Locomotive Locomotive { get; set; } = null!;

        // Must have setters, or EF cannot materialize the entity
        public DateOnly Date { get; set; }

        public Shift Shift { get; set; } = Shift.Day;

        [Required]
        [Column(TypeName = Dec)]
        public decimal InitialValue { get; set; }

        [Required]
        [Column(TypeName = Dec)]
        public decimal FinalValue { get; set; }

        [Required]
        [Column(TypeName = Dec)]
        public decimal Amount { get; set; }

        public DateOnly CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        [StringLength(NoteMaxLength)]
        public string? Note { get; set; }
    }
}