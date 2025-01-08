using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PacienteDependentePlanoMedicoConfiguration : IEntityTypeConfiguration<PacienteDependentePlanoMedico>
{
    public void Configure(EntityTypeBuilder<PacienteDependentePlanoMedico> builder)
    {
        builder.ToTable("PacienteDependentePlanoMedico");

        builder.HasKey(c => c.PlanoMedicoId);

        builder.Property(c => c.PlanoMedicoId).HasColumnName("Id");
        builder.Property(c => c.NomePlano).IsRequired().HasMaxLength(255);
        builder.Property(c => c.NumCartao).IsRequired().HasMaxLength(155);
        builder.Property(c => c.DependenteId).IsRequired();
        builder.Property(c => c.ConvenioMedicoId).IsRequired();

        builder.HasOne(pm => pm.Dependente)
            .WithMany(p => p.PlanosMedicos)
            .HasForeignKey(pm => pm.DependenteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pm => pm.ConvenioMedico)
            .WithMany()
            .HasForeignKey(pm => pm.ConvenioMedicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}