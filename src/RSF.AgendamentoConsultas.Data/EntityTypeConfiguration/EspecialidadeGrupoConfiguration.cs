using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class EspecialidadeGrupoConfiguration : IEntityTypeConfiguration<EspecialidadeGrupo>
{
    public void Configure(EntityTypeBuilder<EspecialidadeGrupo> builder)
    {
        builder.ToTable("GrupoEspecialidade");

        builder.HasKey(c => c.EspecialidadeGrupoId);

        builder.Property(c => c.EspecialidadeGrupoId).HasColumnName("Id");
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(255);
        builder.Property(c => c.NomePlural).HasMaxLength(455);
        builder.Property(c => c.Code).HasMaxLength(455);
    }
}