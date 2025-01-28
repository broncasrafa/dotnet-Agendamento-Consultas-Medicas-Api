using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialista;

public class UpdateEspecialistaRequestHandler : IRequestHandler<UpdateEspecialistaRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;

    public UpdateEspecialistaRequestHandler(IEspecialistaRepository especialistaRepository) => _especialistaRepository = especialistaRepository;

    public async Task<Result<bool>> Handle(UpdateEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        especialista.Update(
            nome: request.NomeCompleto,
            tipo: request.Tipo,
            foto: request.Foto,
            agendaOnline: request.AgendaOnline,
            telemedicinaOnline: request.TelemedicinaOnline,
            telemedicinaAtivo: request.TelemedicinaAtivo,
            telemedicinaPrecoNumber: request.TelemedicinaPreco,
            experienciaProfissional: request.ExperienciaProfissional,
            formacaoAcademica: request.FormacaoAcademica);

        var rowsAffected = await _especialistaRepository.UpdateAsync(especialista);

        return await Task.FromResult(rowsAffected > 0);
    }
}