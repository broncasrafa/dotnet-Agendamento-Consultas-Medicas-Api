using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class EspecialistaPerguntaConfiguration : IEntityTypeConfiguration<EspecialistaPergunta>
{
    public void Configure(EntityTypeBuilder<EspecialistaPergunta> builder)
    {
        builder.ToTable("EspecialistaPergunta");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.EspecialistaId).IsRequired();

        builder.Property(p => p.PacienteId).IsRequired();

        builder.Property(p => p.Titulo).IsRequired().HasMaxLength(145);

        builder.Property(p => p.Pergunta).IsRequired().HasColumnType("varchar(max)");

        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.HasOne(p => p.Especialista)
            .WithMany(e => e.Perguntas)
            .HasForeignKey(p => p.EspecialistaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Paciente)
            .WithMany(c => c.PerguntasRealizadas)
            .HasForeignKey(p => p.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}