using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class EspecialistaEspecialidadeConfiguration : IEntityTypeConfiguration<EspecialistaEspecialidade>
{
    public void Configure(EntityTypeBuilder<EspecialistaEspecialidade> builder)
    {
        builder.ToTable("EspecialistaEspecialidade");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.EspecialidadeId).IsRequired();
        builder.Property(c => c.TipoEspecialidade).IsRequired().HasMaxLength(45);

        builder.HasOne(ee => ee.Especialista)
            .WithMany(e => e.Especialidades)
            .HasForeignKey(ee => ee.EspecialistaId);

        builder.HasOne(ee => ee.Especialidade)
            .WithMany()
            .HasForeignKey(ee => ee.EspecialidadeId);
    }
}