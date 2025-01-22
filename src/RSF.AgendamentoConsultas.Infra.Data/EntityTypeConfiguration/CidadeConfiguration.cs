using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
{
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.ToTable("Cidade");

        builder.HasKey(c => c.CidadeId);

        builder.Property(c => c.CidadeId).HasColumnName("Id");
        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Code).HasMaxLength(255);
        builder.Property(c => c.EstadoId).IsRequired();

        builder.HasOne(c => c.Estado)
            .WithMany(c => c.Cidades)
            .HasForeignKey(c => c.EstadoId);
    }
}