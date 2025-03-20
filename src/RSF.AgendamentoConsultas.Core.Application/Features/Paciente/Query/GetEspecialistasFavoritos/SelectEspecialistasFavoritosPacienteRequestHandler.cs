using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetEspecialistasFavoritos;

public class SelectEspecialistasFavoritosPacienteRequestHandler : IRequestHandler<SelectEspecialistasFavoritosPacienteRequest, Result<PagedResult<EspecialistaResponse>>>
{
    private readonly IPacienteEspecialistaFavoritosRepository _repository;
    private readonly IPacienteRepository _pacienteRepository;

    public SelectEspecialistasFavoritosPacienteRequestHandler(IPacienteEspecialistaFavoritosRepository repository, IPacienteRepository pacienteRepository)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<PagedResult<EspecialistaResponse>>> Handle(SelectEspecialistasFavoritosPacienteRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");
                
        var pagedResult = await _repository.GetAllPagedAsync(request.PacienteId, request.PageNum, request.PageSize);

        var response = EspecialistaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}