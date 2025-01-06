using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class EspecialistaAvaliacaoConfiguration : IEntityTypeConfiguration<EspecialistaAvaliacao>
{
    public void Configure(EntityTypeBuilder<EspecialistaAvaliacao> builder)
    {
        builder.ToTable("EspecialistaAvaliacao");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Feedback).IsRequired().HasColumnType("varchar(max)");
        builder.Property(a => a.Score).IsRequired();
        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.HasOne(a => a.Especialista)
            .WithMany(e => e.Avaliacoes)
            .HasForeignKey(a => a.EspecialistaId);

        builder.HasOne(a => a.Paciente)
            .WithMany(e => e.AvaliacoesFeitas)
            .HasForeignKey(a => a.PacienteId);
    }
}