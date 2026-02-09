using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loco.Domain.Locomotives;
using static Loco.GCommon.EntityValidationConstants.Fuel;
namespace Loco.Domain.Fuel;

public class Fuel
{
    [Key] public int Id { get; set; }
    [ForeignKey(nameof(Locomotive))] public int LocoId { get; set; }
    public Locomotive Locomotive { get; set; } = null!;
    public DateOnly Date { get; set; }

    [Column(TypeName = Dec)]
    public decimal InitialFuel { get; set; }

    [Column(TypeName = Dec)]
    public decimal FinalFuel { get; set; }

    [Column(TypeName = Dec)]
    public decimal Consumption { get; set; }

    [Column(TypeName = Dec)]
    public decimal Refueled { get; set; }

    public DateOnly CreatedOn { get; set; }
    public string? CreatedBy { get; set; }

    [StringLength(NoteMaxLength)]
    public string? Note { get; set; }

    } 
