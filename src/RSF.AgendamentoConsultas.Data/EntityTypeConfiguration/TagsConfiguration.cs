using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class TagsConfiguration : IEntityTypeConfiguration<Tags>
{
    public void Configure(EntityTypeBuilder<Tags> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(c => c.TagId);

        builder.Property(c => c.TagId).HasColumnName("Id");
        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(455);
    }
}