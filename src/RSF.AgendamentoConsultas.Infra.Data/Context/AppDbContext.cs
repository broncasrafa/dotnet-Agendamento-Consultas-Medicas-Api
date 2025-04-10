﻿using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<ConvenioMedico> ConveniosMedicos { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<EspecialidadeGrupo> EspecialidadesGrupos { get; set; }
    public DbSet<TipoConsulta> TiposConsulta { get; set; }
    public DbSet<TipoStatusConsulta> TiposStatusConsulta { get; set; }
    public DbSet<TipoAgendamento> TiposAgendamento { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Especialista> Especialistas { get; set; }
    public DbSet<AgendamentoConsulta> Agendamentos { get; set; }
    public DbSet<Pergunta> Perguntas { get; set; }
    public DbSet<PerguntaResposta> Respostas { get; set; }
    public DbSet<PacienteEspecialistaFavoritos> Favoritos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}