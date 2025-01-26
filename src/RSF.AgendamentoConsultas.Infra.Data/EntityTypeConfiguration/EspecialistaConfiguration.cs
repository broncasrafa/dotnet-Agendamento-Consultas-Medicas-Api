using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class EspecialistaConfiguration : IEntityTypeConfiguration<Especialista>
{
    public void Configure(EntityTypeBuilder<Especialista> builder)
    {
        builder.ToTable("Especialista");

        builder.HasKey(e => e.EspecialistaId);

        builder.Property(c => c.EspecialistaId).HasColumnName("Id");
        builder.Property(c => c.UserId).HasMaxLength(455);
        builder.Property(c => c.Tipo).HasMaxLength(455);
        builder.Property(c => c.Code).HasMaxLength(455);
        builder.Property(c => c.CodeId).HasMaxLength(455);
        builder.Property(c => c.EspecCodeId).HasMaxLength(455);
        builder.Property(c => c.Nome).HasMaxLength(455);
        builder.Property(c => c.Licenca).HasMaxLength(455);
        builder.Property(c => c.Email).HasMaxLength(455);
        builder.Property(c => c.Foto).HasColumnType("varchar(max)");
        builder.Property(c => c.SharedUrl).HasMaxLength(455);
        builder.Property(c => c.Ativo).HasColumnType("bit");
        builder.Property(c => c.AgendaOnline).HasColumnType("bit");
        builder.Property(c => c.PerfilVerificado).HasColumnType("bit");
        builder.Property(c => c.PermitirPergunta).HasColumnType("bit");
        builder.Property(c => c.TelemedicinaOnline).HasColumnType("bit");
        builder.Property(c => c.TelemedicinaAtivo).HasColumnType("bit");
        builder.Property(c => c.TelemedicinaPreco).HasMaxLength(455);
        builder.Property(c => c.TelemedicinaPrecoNumber).HasColumnType("decimal(18,2)");
        builder.Property(c => c.Avaliacao).HasColumnType("decimal(2,1)");
        builder.Property(c => c.ExperienciaProfissional).HasColumnType("varchar(max)");
        builder.Property(c => c.FormacaoAcademica).HasColumnType("varchar(max)");
        builder.Property(c => c.Genero).HasMaxLength(455);
        builder.Property(c => c.Tratamento).HasMaxLength(455);

        builder.HasMany(e => e.Especialidades)
               .WithOne(ee => ee.Especialista)
               .HasForeignKey(ee => ee.EspecialistaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ConveniosMedicosAtendidos)
               .WithOne(ecm => ecm.Especialista)
               .HasForeignKey(ecm => ecm.EspecialistaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.LocaisAtendimento)
               .WithOne(l => l.Especialista)
               .HasForeignKey(l => l.EspecialistaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Tags)
               .WithOne(et => et.Especialista)
               .HasForeignKey(et => et.EspecialistaId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Avaliacoes)
               .WithOne(ea => ea.Especialista)
               .HasForeignKey(ea => ea.EspecialistaId)
               .OnDelete(DeleteBehavior.Restrict); // Evitar deleção em cascata

        builder.HasMany(e => e.Respostas)
               .WithOne(ep => ep.Especialista)
               .HasForeignKey(ep => ep.EspecialistaId)
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(cs => cs.ConsultasAtendidas)
            .WithOne(e=>e.Especialista)
            .HasForeignKey(e => e.EspecialistaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}