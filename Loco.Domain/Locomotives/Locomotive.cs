using System.ComponentModel.DataAnnotations;
using Loco.Domain.Work;
using Loco.GCommon.Enums;
using Microsoft.EntityFrameworkCore;

namespace Loco.Domain.Locomotives;
using static GCommon.EntityValidationConstants.Locomotive;

[Index(nameof(Number), IsUnique = true)]
public class Locomotive
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(LocomotiveNumberLength)]
    public string Number { get; set; } = null!;

    public LocomotiveType LocomotiveType { get; set; } = LocomotiveType.Shunter;

    [Comment(" Mh or Km")]
    public MeasuringUnits MeasuringUnit { get; set; } = MeasuringUnits.Mh;
     
    [StringLength(CreatedByMaxLength)]
    public string? CreatedBy { get; set; } = "Admin";

    public DateOnly CreatedOn { get; set; }  

    public bool IsDeleted { get; set; } = false;

    [StringLength(NoteMaxLength)]
    public string? Note { get; set; } 

    public virtual ICollection<ShiftWork> ShiftWorks { get; set; }
        = new HashSet<ShiftWork>();

    public virtual ICollection<Fuel.Fuel> Fuels { get; set; }
        = new HashSet<Fuel.Fuel>();

    }