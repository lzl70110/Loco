using Loco.Domain.Fuel;
using Loco.Domain.Locomotives;
using Loco.Domain.Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loco.Infrastructure.Persistence;


public sealed class LocoDbContext : IdentityDbContext<IdentityUser>
{
    public LocoDbContext(DbContextOptions<LocoDbContext> options) : base(options) { }

    public DbSet<Locomotive> Locomotives => Set<Locomotive>();
    public DbSet<ShiftWork> ShiftWorks => Set<ShiftWork>();
    public DbSet<Fuel> Fuels => Set<Fuel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        // Apply all IEntityTypeConfiguration<> from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
        // Single timestamp for consistency
        var nowDateOnly = DateOnly.FromDateTime(DateTime.UtcNow);

        // -----------------------------
        // SHIFTWORK : set CreatedOn
        // -----------------------------
        foreach (var e in ChangeTracker.Entries<ShiftWork>())
            {
            if (e.State == EntityState.Added && e.Entity.CreatedOn == default)
                e.Entity.CreatedOn = nowDateOnly;
            }

        // -----------------------------
        // LOCOMOTIVE : set CreatedOn
        // -----------------------------
        foreach (var e in ChangeTracker.Entries<Locomotive>())
            {
            if (e.State == EntityState.Added && e.Entity.CreatedOn == default)
                e.Entity.CreatedOn = nowDateOnly;
            }

        // -----------------------------
        // FUEL : business rules (Variant B)
        // -----------------------------
        foreach (var e in ChangeTracker.Entries<Fuel>())
            {
            if (e.State == EntityState.Added || e.State == EntityState.Modified)
                {
                var f = e.Entity;

                // 1) Calculate Consumption BEFORE save
                f.Consumption = f.InitialFuel + f.Refueled - f.FinalFuel;

                // 2) CreatedOn only on insert
                if (e.State == EntityState.Added && f.CreatedOn == default)
                    f.CreatedOn = nowDateOnly;
                }
            }

        return await base.SaveChangesAsync(ct);
        }
    }