using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories;
using RSF.AgendamentoConsultas.Infra.Data.Tests.Base;
using RSF.AgendamentoConsultas.Infra.Data.Tests.Fixtures;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Infra.Data.Tests.Repositories;

[Collection(nameof(AgendamentoConsultaRepositoryTestFixture))]
public class AgendamentoConsultaRepositoryTest
{
    private readonly AppDbContext _context;
    private readonly AgendamentoConsultaRepository _repository;
    private readonly AgendamentoConsultaRepositoryTestFixture _fixture;

    public AgendamentoConsultaRepositoryTest(AgendamentoConsultaRepositoryTestFixture fixture)
    {
        _context = BaseFixture.CreateDbContext();
        // Garante a recriação do banco para cada teste
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _repository = new AgendamentoConsultaRepository(_context);
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAllExpiredByPacienteAsync_DeveRetornarConsultasExpiradas()
    {
        // Arrange
        var agendamentos = _fixture.GetAgendamentosExpiradosPaciente();
        _context.Agendamentos.AddRange(agendamentos);
        await _context.SaveChangesAsync();

        agendamentos.FirstOrDefault(c => c.AgendamentoConsultaId == 1)!.ConfirmedByEspecialistaAt = DateTime.Now.AddDays(4);
        agendamentos.FirstOrDefault(c => c.AgendamentoConsultaId == 2)!.ConfirmedByEspecialistaAt = DateTime.Now.AddDays(9);
        _context.Agendamentos.UpdateRange(agendamentos);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _repository.GetAllExpiredByPacienteAsync();

        // Assert
        resultado.Should().HaveCount(2).And.OnlyContain(x => x.AgendamentoConsultaId == 1 || x.AgendamentoConsultaId == 2);
    }

    [Fact]
    public async Task GetAllExpiredByEspecialistaAsync_DeveRetornarConsultasExpiradas()
    {
        // Arrange
        var agendamentos = _fixture.GetAgendamentosExpiradosPaciente();
        _context.Agendamentos.AddRange(agendamentos);
        await _context.SaveChangesAsync();

        agendamentos.FirstOrDefault(c => c.AgendamentoConsultaId == 1)!.DataConsulta = DateTime.Now.AddDays(-1);
        agendamentos.FirstOrDefault(c => c.AgendamentoConsultaId == 2)!.DataConsulta = DateTime.Now.AddDays(-1);
        _context.Agendamentos.UpdateRange(agendamentos);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _repository.GetAllExpiredByEspecialistaAsync();

        // Assert
        resultado.Should().HaveCount(2).And.OnlyContain(x => x.AgendamentoConsultaId == 1 || x.AgendamentoConsultaId == 2);
    }
}