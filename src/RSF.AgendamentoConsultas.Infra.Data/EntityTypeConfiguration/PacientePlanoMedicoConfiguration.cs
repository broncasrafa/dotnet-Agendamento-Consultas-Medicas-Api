using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class PacientePlanoMedicoConfiguration : IEntityTypeConfiguration<PacientePlanoMedico>
{
    public void Configure(EntityTypeBuilder<PacientePlanoMedico> builder)
    {
        builder.ToTable("PacientePlanoMedico");

        builder.HasKey(c => c.PlanoMedicoId);

        builder.Property(c => c.PlanoMedicoId).HasColumnName("Id");
        builder.Property(c => c.NomePlano).IsRequired().HasMaxLength(255);
        builder.Property(c => c.NumCartao).IsRequired().HasMaxLength(155);
        builder.Property(c => c.PacienteId).IsRequired();
        builder.Property(c => c.ConvenioMedicoId).IsRequired();
        builder.Property(c => c.IsActive).IsRequired().HasColumnType("bit").HasDefaultValueSql("((1))");

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