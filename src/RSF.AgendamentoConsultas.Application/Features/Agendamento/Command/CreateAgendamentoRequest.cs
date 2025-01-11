using MediatR;

using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command;

public record CreateAgendamentoRequest(
    int EspecialistaId,
    int EspecialidadeId,
    int LocalAtendimentoId,
    int TipoConsultaId,
    int TipoAgendamentoId,
    DateTime DataConsulta,
    string HorarioConsulta,
    string MotivoConsulta,
    decimal? ValorConsulta,
    string TelefoneContato,
    bool PrimeiraVez,
    int PacienteId,
    int? DependenteId,
    int PlanoMedicoId

    ) : IRequest<Result<int>>;