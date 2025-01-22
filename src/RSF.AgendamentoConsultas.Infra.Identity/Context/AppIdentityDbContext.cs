using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Identity.Context;

public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Renomeando as tabelas do Identity
        builder.Entity<ApplicationUser>(entity => entity.ToTable("IdentityUser"));
        builder.Entity<IdentityRole>(entity => entity.ToTable("IdentityRole"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("IdentityUserRole"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("IdentityUserClaim"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("IdentityUserLogin"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("IdentityRoleClaim"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("IdentityUserToken"));
    }
}