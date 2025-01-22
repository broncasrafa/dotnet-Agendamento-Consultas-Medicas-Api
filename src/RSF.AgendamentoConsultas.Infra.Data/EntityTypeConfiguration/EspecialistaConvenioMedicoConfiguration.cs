using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class EspecialistaConvenioMedicoConfiguration : IEntityTypeConfiguration<EspecialistaConvenioMedico>
{
    public void Configure(EntityTypeBuilder<EspecialistaConvenioMedico> builder)
    {
        builder.ToTable("EspecialistaConvenioMedico");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.ConvenioMedicoId).IsRequired();

        builder.HasOne(ecm => ecm.Especialista)
               .WithMany(e => e.ConveniosMedicosAtendidos)
               .HasForeignKey(ecm => ecm.EspecialistaId);

        builder.HasOne(ecm => ecm.ConvenioMedico)
               .WithMany()
               .HasForeignKey(ecm => ecm.ConvenioMedicoId);
    }
}