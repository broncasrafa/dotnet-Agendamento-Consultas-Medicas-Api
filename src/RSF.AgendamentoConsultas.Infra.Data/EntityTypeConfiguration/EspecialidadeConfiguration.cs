using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class EspecialidadeConfiguration : IEntityTypeConfiguration<Especialidade>
{
    public void Configure(EntityTypeBuilder<Especialidade> builder)
    {
        builder.ToTable("Especialidade");

        builder.HasKey(c => c.EspecialidadeId);

        builder.Property(c => c.EspecialidadeId).HasColumnName("Id");
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(255);
        builder.Property(c => c.NomePlural).HasMaxLength(455);
        builder.Property(c => c.Term).HasMaxLength(455);
        builder.Property(c => c.Code).HasMaxLength(255);
        builder.Property(c => c.CodeOld).HasMaxLength(255);
        builder.Property(c => c.EspecialidadeGrupoId).IsRequired().HasColumnName("GrupoEspecialidadeId");

        builder.HasOne(g => g.EspecialidadeGrupo)
            .WithMany(e => e.Especialidades)
            .HasForeignKey(g => g.EspecialidadeGrupoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}