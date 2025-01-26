using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

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
        builder.Property(c => c.AgendamentoDependente).IsRequired().HasColumnType("bit").HasDefaultValueSql("((0))");
        builder.Property(c => c.PacienteId).IsRequired();
        builder.Property(c => c.PlanoMedicoId).IsRequired(false);
        builder.Property(c => c.DependenteId).IsRequired(false);
        builder.Property(c => c.DependentePlanoMedicoId).IsRequired(false);
        builder.Property(c => c.TipoConsultaId).IsRequired();
        builder.Property(c => c.TipoAgendamentoId).IsRequired();
        builder.Property(c => c.StatusConsultaId).IsRequired();
        builder.Property(c => c.TelefoneContato).IsRequired().HasMaxLength(20);
        builder.Property(c => c.MotivoConsulta).IsRequired().HasMaxLength(455);
        builder.Property(c => c.DataConsulta).IsRequired().HasColumnType("datetime");
        builder.Property(c => c.HorarioConsulta).IsRequired().HasMaxLength(45);
        builder.Property(c => c.ValorConsulta).IsRequired(false).HasColumnType("decimal(18,2)"); ;
        builder.Property(c => c.PrimeiraVez).IsRequired().HasColumnType("bit").HasDefaultValueSql("((0))");
        builder.Property(c => c.DuracaoEmMinutosConsulta).IsRequired(false).HasColumnType("int");
        builder.Property(c => c.Observacoes).IsRequired(false).HasColumnType("varchar(max)");
        builder.Property(c => c.NotaCancelamento).IsRequired(false).HasMaxLength(1000);
        builder.Property(c => c.ConfirmedByPacienteAt).IsRequired(false).HasColumnType("datetime");
        builder.Property(c => c.ConfirmedByEspecialistaAt).IsRequired(false).HasColumnType("datetime");
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        builder.Property(c => c.UpdatedAt).IsRequired(false).HasColumnType("datetime");


        // Relacionamentos
        builder.HasOne(a => a.Especialista)
            .WithMany(cs => cs.ConsultasAtendidas) // Relacionamento com Especialista
            .HasForeignKey(a => a.EspecialistaId)
            .OnDelete(DeleteBehavior.Restrict);  // Não permitir exclusão em cascata

        builder.HasOne(a => a.Especialidade)
            .WithMany() // Relacionamento com Especialidade
            .HasForeignKey(a => a.EspecialidadeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.LocalAtendimento)
            .WithMany() // Relacionamento com EspecialistaLocalAtendimento
            .HasForeignKey(a => a.LocalAtendimentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.StatusConsulta)
            .WithMany() // Relacionamento com TipoStatusConsulta
            .HasForeignKey(a => a.StatusConsultaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.TipoConsulta)
            .WithMany() // Relacionamento com TipoConsulta
            .HasForeignKey(a => a.TipoConsultaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.TipoAgendamento)
            .WithMany() // Relacionamento com TipoAgendamento
            .HasForeignKey(a => a.TipoAgendamentoId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasOne(a => a.Paciente)
            .WithMany(p => p.AgendamentosRealizados)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.SetNull); // Quando o paciente for excluído, os agendamentos serão mantidos com PacienteId nulo

        builder.HasOne(a => a.PlanoMedico)
            .WithMany() // Relacionamento com PacientePlanoMedico
            .HasForeignKey(a => a.PlanoMedicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Dependente)
            .WithMany(p => p.AgendamentosRealizados)
            .HasForeignKey(a => a.DependenteId)
            .OnDelete(DeleteBehavior.SetNull); // Quando o dependente for excluído, os agendamentos serão mantidos com DependenteId nulo

        builder.HasOne(a => a.PlanoMedicoDependente)
            .WithMany() // Relacionamento com PacienteDependentePlanoMedico
            .HasForeignKey(a => a.DependentePlanoMedicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}