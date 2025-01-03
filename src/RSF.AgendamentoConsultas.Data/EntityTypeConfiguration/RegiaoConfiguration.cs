using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;


namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class RegiaoConfiguration : IEntityTypeConfiguration<Regiao>
{
    public void Configure(EntityTypeBuilder<Regiao> builder)
    {
        builder.ToTable("Regiao");

        builder.HasKey(c => c.RegiaoId);

        builder.Property(c => c.RegiaoId).HasColumnName("Id");

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(100);
    }
}