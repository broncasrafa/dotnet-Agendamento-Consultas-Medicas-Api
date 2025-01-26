using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteLocalAtendimento;

public class DeleteLocalAtendimentoRequestHandler : IRequestHandler<DeleteLocalAtendimentoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> _especialistaLocalAtendimentoRepository;

    public DeleteLocalAtendimentoRequestHandler(
        IEspecialistaRepository especialistaRepository, 
        IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> especialistaLocalAtendimentoRepository)
    {
        _especialistaRepository = especialistaRepository;
        _especialistaLocalAtendimentoRepository = especialistaLocalAtendimentoRepository;
    }

    public async Task<Result<bool>> Handle(DeleteLocalAtendimentoRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var localAtendimento = especialista.LocaisAtendimento.FirstOrDefault(c => c.Id == request.LocalAtendimentoId);
        NotFoundException.ThrowIfNull(localAtendimento,
            $"Local de Atendimento com o ID: '{request.LocalAtendimentoId}' não encontrado para o Especialista com o ID: '{request.EspecialistaId}'");

        var rowsAffected = await _especialistaLocalAtendimentoRepository.RemoveAsync(localAtendimento);

        return await Task.FromResult(rowsAffected > 0);
    }
}