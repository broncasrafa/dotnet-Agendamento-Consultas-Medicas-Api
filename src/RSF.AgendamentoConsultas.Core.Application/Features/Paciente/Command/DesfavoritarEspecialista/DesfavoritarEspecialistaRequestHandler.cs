using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DesfavoritarEspecialista;

public class DesfavoritarEspecialistaRequestHandler : IRequestHandler<DesfavoritarEspecialistaRequest, Result<bool>>
{
    private readonly IPacienteEspecialistaFavoritosRepository _repository;

    public DesfavoritarEspecialistaRequestHandler(IPacienteEspecialistaFavoritosRepository repository) => _repository = repository;

    public async Task<Result<bool>> Handle(DesfavoritarEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var favorito = await _repository.GetByIdsAsync(request.PacienteId, request.EspecialistaId);
        NotFoundException.ThrowIfNull(favorito, $"Favoritagem não foi encontrada");
        
        var rowsAffected = await _repository.RemoveAsync(favorito);

        return await Task.FromResult(rowsAffected > 0);
    }
}