using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PerguntaConfiguration : IEntityTypeConfiguration<Pergunta>
{
    public void Configure(EntityTypeBuilder<Pergunta> builder)
    {
        builder.ToTable("Pergunta");

        builder.HasKey(c => c.PerguntaId);

        builder.Property(c => c.EspecialidadeId).IsRequired();
        builder.Property(c => c.PacienteId).IsRequired();
        builder.Property(c => c.Texto).IsRequired().HasColumnName("Pergunta").HasMaxLength(450);
        builder.Property(c => c.TermosUsoPolitica).IsRequired().HasColumnType("bit").HasDefaultValueSql("((1))");
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

        builder.HasOne(p => p.Especialidade)
            .WithMany(e => e.Perguntas)
            .HasForeignKey(p => p.EspecialidadeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Paciente)
            .WithMany(c => c.PerguntasRealizadas)
            .HasForeignKey(p => p.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}