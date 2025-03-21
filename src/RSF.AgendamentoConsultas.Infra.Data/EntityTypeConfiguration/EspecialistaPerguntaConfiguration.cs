using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class EspecialistaPerguntaConfiguration : IEntityTypeConfiguration<EspecialistaPergunta>
{
    public void Configure(EntityTypeBuilder<EspecialistaPergunta> builder)
    {
        builder.ToTable("EspecialistaPergunta");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.PerguntaId).IsRequired();
        
        // Relacionamentos
        builder.HasOne(a => a.Especialista)
            .WithMany(p => p.PerguntasEspecialista)
            .HasForeignKey(a => a.EspecialistaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Pergunta)
            .WithMany()
            .HasForeignKey(a => a.PerguntaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}