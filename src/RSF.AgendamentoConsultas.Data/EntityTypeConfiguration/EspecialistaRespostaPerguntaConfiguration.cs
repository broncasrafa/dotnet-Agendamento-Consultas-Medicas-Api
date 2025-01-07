using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class EspecialistaRespostaPerguntaConfiguration : IEntityTypeConfiguration<EspecialistaRespostaPergunta>
{
    public void Configure(EntityTypeBuilder<EspecialistaRespostaPergunta> builder)
    {
        builder.ToTable("EspecialistaRespostaPergunta");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PerguntaId).IsRequired();

        builder.Property(r => r.Resposta).IsRequired().HasColumnType("varchar(max)");
        
        builder.Property(r => r.Likes).HasDefaultValue(0);

        builder.Property(r => r.Dislikes).HasDefaultValue(0);

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");


        builder.HasOne(p => p.Pergunta)
            .WithMany(r => r.Respostas)
            .HasForeignKey(p => p.PerguntaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}