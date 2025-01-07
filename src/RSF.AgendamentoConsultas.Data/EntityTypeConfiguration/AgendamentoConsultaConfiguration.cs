﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class AgendamentoConsultaConfiguration : IEntityTypeConfiguration<AgendamentoConsulta>
{
    public void Configure(EntityTypeBuilder<AgendamentoConsulta> builder)
    {
        builder.ToTable("AgendamentoConsulta");

        builder.HasKey(c => c.AgendamentoConsultaId);

        builder.Property(c => c.AgendamentoConsultaId).HasColumnName("Id");
        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.EspecialidadeId).IsRequired();
        builder.Property(c => c.LocalAtendimentoId).IsRequired();
        builder.Property(c => c.PacienteId).IsRequired();
        builder.Property(c => c.PlanoMedicoId).IsRequired();
        builder.Property(c => c.StatusConsultaId).IsRequired();
        builder.Property(c => c.DataConsulta).IsRequired().HasColumnType("datetime");
        builder.Property(c => c.HorarioConsulta).IsRequired().HasMaxLength(45);
        builder.Property(c => c.PrimeiraVez).IsRequired().HasColumnType("bit").HasDefaultValueSql("((0))");
        builder.Property(c => c.DuracaoEmMinutosConsulta).HasColumnType("int");
        builder.Property(c => c.Observacoes).HasColumnType("varchar(max)");
        builder.Property(c => c.NotaCancelamento).HasMaxLength(1000);
        builder.Property(c => c.ConfirmedAt).HasColumnType("datetime");
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");
        builder.Property(c => c.UpdatedAt).HasColumnType("datetime");

        // Relacionamentos
        builder.HasOne(a => a.Especialista)
            .WithMany() // Relacionamento com Especialista
            .HasForeignKey(a => a.EspecialistaId)
            .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata

        builder.HasOne(a => a.Especialidade)
            .WithMany() // Relacionamento com Especialidade
            .HasForeignKey(a => a.EspecialidadeId)
            .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata

        builder.HasOne(a => a.LocalAtendimento)
            .WithMany() // Relacionamento com EspecialistaLocalAtendimento
            .HasForeignKey(a => a.LocalAtendimentoId)
            .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata

        builder.HasOne(a => a.Paciente)
            .WithMany(p => p.AgendamentosRealizados)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.SetNull); // Quando o paciente for excluído, os agendamentos serão mantidos com PacienteId nulo

        builder.HasOne(a => a.PlanoMedico)
            .WithMany() // Relacionamento com PacientePlanoMedico
            .HasForeignKey(a => a.PlanoMedicoId)
            .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata

        builder.HasOne(a => a.StatusConsulta)
            .WithMany() // Relacionamento com TipoStatusConsulta
            .HasForeignKey(a => a.StatusConsultaId)
            .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata
    }
}