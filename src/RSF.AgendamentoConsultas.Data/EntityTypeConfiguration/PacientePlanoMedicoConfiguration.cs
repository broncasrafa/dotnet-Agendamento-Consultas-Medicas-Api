using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PacientePlanoMedicoConfiguration : IEntityTypeConfiguration<PacientePlanoMedico>
{
    public void Configure(EntityTypeBuilder<PacientePlanoMedico> builder)
    {
        builder.HasKey(pm => pm.PlanoMedicoId);

        builder.Property(pm => pm.NomePlano)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pm => pm.NumCartao)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(pm => pm.Paciente)
            .WithMany(p => p.PlanosMedicos)
            .HasForeignKey(pm => pm.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pm => pm.ConvenioMedico)
            .WithMany()
            .HasForeignKey(pm => pm.ConvenioMedicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}