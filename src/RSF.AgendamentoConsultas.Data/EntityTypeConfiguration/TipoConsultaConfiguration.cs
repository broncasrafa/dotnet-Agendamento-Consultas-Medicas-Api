using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class TipoConsultaConfiguration : IEntityTypeConfiguration<TipoConsulta>
{
    public void Configure(EntityTypeBuilder<TipoConsulta> builder)
    {
        builder.ToTable("TipoConsulta");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(255);
    }
}