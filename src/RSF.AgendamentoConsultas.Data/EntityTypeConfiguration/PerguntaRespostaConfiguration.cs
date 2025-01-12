using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PerguntaRespostaConfiguration : IEntityTypeConfiguration<PerguntaResposta>
{
    public void Configure(EntityTypeBuilder<PerguntaResposta> builder)
    {
        builder.ToTable("PerguntaResposta");

        builder.HasKey(c => c.PerguntaRespostaId);

        builder.Property(c => c.PerguntaId).IsRequired();
        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.Resposta).IsRequired().HasColumnType("varchar(450)");                
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");


        builder.HasOne(p => p.Pergunta)
            .WithMany(r => r.Respostas)
            .HasForeignKey(p => p.PerguntaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Especialista)
            .WithMany(r => r.Respostas)
            .HasForeignKey(e => e.EspecialistaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}