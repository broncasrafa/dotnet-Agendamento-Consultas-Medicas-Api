using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;


namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class EspecialistaTagsConfiguration : IEntityTypeConfiguration<EspecialistaTags>
{
    public void Configure(EntityTypeBuilder<EspecialistaTags> builder)
    {
        builder.ToTable("EspecialistaTags");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.TagsId).IsRequired();

        //builder.HasOne(e => e.Especialista)
        //    .WithMany(t => t.Tags)
        //    .HasForeignKey(e => e.EspecialistaId);

        builder.HasOne(et => et.Tag)
            .WithMany()
            .HasForeignKey(et => et.TagsId);
    }
}