using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class TipoAgendamentoConfiguration : IEntityTypeConfiguration<TipoAgendamento>
{
    public void Configure(EntityTypeBuilder<TipoAgendamento> builder)
    {
        builder.ToTable("TipoAgendamento");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(255);
    }
}