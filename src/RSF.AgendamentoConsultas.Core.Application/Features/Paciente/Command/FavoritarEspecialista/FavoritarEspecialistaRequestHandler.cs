using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.FavoritarEspecialista;

public class FavoritarEspecialistaRequestHandler : IRequestHandler<FavoritarEspecialistaRequest, Result<bool>>
{
    private readonly IPacienteEspecialistaFavoritosRepository _repository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;

    public FavoritarEspecialistaRequestHandler(
        IPacienteEspecialistaFavoritosRepository repository, 
        IPacienteRepository pacienteRepository, 
        IEspecialistaRepository especialistaRepository)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
        _especialistaRepository = especialistaRepository;
    }

    public async Task<Result<bool>> Handle(FavoritarEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var pacienteExists = await _pacienteRepository.ExistsByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNotExists(pacienteExists, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var especialistaExists = await _especialistaRepository.ExistsByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNotExists(especialistaExists, $"Especialista com o ID: '{request.EspecialistaId}' não foi encontrado");

        var favoritoExists = await _repository.GetByIdsAsync(request.PacienteId, request.EspecialistaId);
        AlreadyExistsException.ThrowIfExists(favoritoExists, $"Especialista com o ID: '{request.EspecialistaId}' já foi favoritado");

        var favorito = new PacienteEspecialistaFavoritos(request.PacienteId, request.EspecialistaId);
        
        var rowsAffected = await _repository.AddAsync(favorito);

        return await Task.FromResult(rowsAffected > 0);
    }
}