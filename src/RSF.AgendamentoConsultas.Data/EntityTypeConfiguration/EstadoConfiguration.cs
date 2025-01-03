using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("Estado");

        builder.HasKey(c => c.EstadoId);

        builder.Property(c => c.EstadoId).HasColumnName("Id");

        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Sigla).IsRequired().HasMaxLength(2);
        builder.Property(c => c.RegiaoId).IsRequired();

        builder.HasOne(c => c.Regiao)
            .WithMany(c => c.Estados)
            .HasForeignKey(c => c.RegiaoId);
    }
}