using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class ConvenioMedicoCidadeConfiguration : IEntityTypeConfiguration<ConvenioMedicoCidade>
{
    public void Configure(EntityTypeBuilder<ConvenioMedicoCidade> builder)
    {
        builder.ToTable("ConvenioMedicoCidade");

        builder.HasKey(cmc => cmc.ConvenioMedicoCidadeId);

        builder.Property(cmc => cmc.ConvenioMedicoCidadeId).HasColumnName("Id");

        builder.HasOne(cmc => cmc.ConvenioMedico)
               .WithMany()
               .HasForeignKey(cmc => cmc.ConvenioMedicoId);

        builder.HasOne(cmc => cmc.Cidade)
               .WithMany()
               .HasForeignKey(cmc => cmc.CidadeId);

        builder.HasOne(cmc => cmc.Estado)
               .WithMany()
               .HasForeignKey(cmc => cmc.EstadoId);
    }
}