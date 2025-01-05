using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class ConvenioMedicoConfiguration : IEntityTypeConfiguration<ConvenioMedico>
{
    public void Configure(EntityTypeBuilder<ConvenioMedico> builder)
    {
        builder.ToTable("ConvenioMedico");

        builder.HasKey(c => c.ConvenioMedicoId);

        builder.Property(c => c.ConvenioMedicoId).HasColumnName("Id");
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(455);
        builder.Property(c => c.Code).HasMaxLength(455);
        builder.Property(c => c.CodeOld).HasMaxLength(1000);

        builder.HasMany(c => c.CidadesAtendidas)
            .WithMany()
            .UsingEntity<ConvenioMedicoCidade>(
                j => j.HasOne(cmc => cmc.Cidade).WithMany().HasForeignKey(cmc => cmc.CidadeId),
                j => j.HasOne(cmc => cmc.ConvenioMedico).WithMany().HasForeignKey(cmc => cmc.ConvenioMedicoId)
            );
    }
}