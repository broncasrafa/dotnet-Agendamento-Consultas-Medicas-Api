using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Paciente");

        builder.HasKey(e => e.PacienteId);

        builder.Property(c => c.PacienteId).HasColumnName("Id");
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(255);
        builder.Property(c => c.CPF).HasMaxLength(11);
        builder.Property(c => c.Email).HasMaxLength(255);
        builder.Property(c => c.Telefone).HasMaxLength(20);
        builder.Property(c => c.Genero).HasMaxLength(45);
        builder.Property(c => c.DataNascimento).IsRequired().HasMaxLength(15);
        builder.Property(c => c.NomeSocial).HasMaxLength(255);
        builder.Property(c => c.Peso).HasColumnType("decimal(4, 1)");
        builder.Property(c => c.Altura).HasColumnType("decimal(3, 2)");
        builder.Property(c => c.TelefoneVerificado).HasColumnType("bit").HasDefaultValueSql("((0))");
        builder.Property(c => c.EmailVerificado).HasColumnType("bit").HasDefaultValueSql("((0))");
        builder.Property(c => c.TermoUsoAceito).HasColumnType("bit").HasDefaultValueSql("((0))");
        builder.Property(c => c.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("(getdate())").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnType("datetime");
        builder.Property(c => c.Password).HasColumnName("PasswordHash").IsRequired(false);
        builder.Property(c => c.IsActive).IsRequired().HasColumnType("bit").HasDefaultValueSql("((1))");

        builder.HasMany(p => p.PlanosMedicos)
            .WithOne(pm => pm.Paciente)
            .HasForeignKey(pm => pm.PacienteId)
            .OnDelete(DeleteBehavior.Cascade); // Caso um paciente seja excluído, seus planos médicos também serão excluídos

        builder.HasMany(p => p.AvaliacoesFeitas)
            .WithOne(a => a.Paciente)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.SetNull); // Quando um paciente for excluído, as avaliações podem ser mantidas com o campo PacienteId null

        builder.HasMany(p => p.PerguntasRealizadas)
            .WithOne(pr => pr.Paciente)
            .HasForeignKey(pr => pr.PacienteId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.AgendamentosRealizados)
            .WithOne(ac => ac.Paciente)
            .HasForeignKey(ac => ac.PacienteId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Dependentes)
            .WithOne(pm => pm.Paciente)
            .HasForeignKey(pm => pm.PacientePrincipalId)
            .OnDelete(DeleteBehavior.Cascade); // Caso um paciente seja excluído, seus dependentes também serão excluídos
    }
}