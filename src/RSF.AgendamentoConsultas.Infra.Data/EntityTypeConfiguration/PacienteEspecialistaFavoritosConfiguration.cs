using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class PacienteEspecialistaFavoritosConfiguration : IEntityTypeConfiguration<PacienteEspecialistaFavoritos>
{
    public void Configure(EntityTypeBuilder<PacienteEspecialistaFavoritos> builder)
    {
        builder.ToTable("PacienteEspecialistaFavoritos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.PacienteId).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");

        // Relacionamentos
        builder.HasOne(a => a.Especialista)
            .WithMany() // Relacionamento com Especialista
            .HasForeignKey(a => a.EspecialistaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Paciente)
            .WithMany(p => p.EspecialistasFavoritos)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}