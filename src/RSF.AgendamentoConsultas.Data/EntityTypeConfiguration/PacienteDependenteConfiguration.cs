using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;


namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PacienteDependenteConfiguration : IEntityTypeConfiguration<PacienteDependente>
{
    public void Configure(EntityTypeBuilder<PacienteDependente> builder)
    {
        builder.ToTable("PacienteDependente");

        builder.HasKey(e => e.DependenteId);

        builder.Property(c => c.DependenteId).HasColumnName("Id");
        builder.Property(c => c.PacientePrincipalId).IsRequired();
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(255);
        builder.Property(c => c.CPF).HasMaxLength(11);
        builder.Property(c => c.Email).HasMaxLength(255);
        builder.Property(c => c.Telefone).HasMaxLength(20);
        builder.Property(c => c.Genero).HasMaxLength(45);
        builder.Property(c => c.DataNascimento).IsRequired().HasMaxLength(15);
        builder.Property(c => c.NomeSocial).HasMaxLength(255);
        builder.Property(c => c.Peso).HasColumnType("decimal(4, 1)");
        builder.Property(c => c.Altura).HasColumnType("decimal(3, 2)");
        builder.Property(c => c.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("(getdate())").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnType("datetime");
        builder.Property(c => c.IsActive).IsRequired().HasColumnType("bit").HasDefaultValueSql("((1))");

        builder.HasMany(p => p.PlanosMedicos)
            .WithOne(pm => pm.Dependente)
            .HasForeignKey(pm => pm.DependenteId)
            .OnDelete(DeleteBehavior.Cascade); // Caso um dependente seja excluído, seus planos médicos também serão excluídos
    }
}