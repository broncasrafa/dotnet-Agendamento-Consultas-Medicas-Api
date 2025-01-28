using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialista;

public record UpdateEspecialistaRequest(
    int EspecialistaId,
    string Tipo,
    string NomeCompleto,
    string Foto,
    bool? AgendaOnline,
    bool? TelemedicinaOnline,
    bool? TelemedicinaAtivo,
    decimal? TelemedicinaPreco,
    string ExperienciaProfissional,
    string FormacaoAcademica
    ) : IRequest<Result<bool>>;



