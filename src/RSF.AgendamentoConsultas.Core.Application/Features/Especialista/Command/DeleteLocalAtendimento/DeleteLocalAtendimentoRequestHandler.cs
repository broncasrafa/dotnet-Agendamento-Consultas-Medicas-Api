using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteLocalAtendimento;

public class DeleteLocalAtendimentoRequestHandler : IRequestHandler<DeleteLocalAtendimentoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> _especialistaLocalAtendimentoRepository;
    private readonly IHttpContextAccessor _httpContext;

    public DeleteLocalAtendimentoRequestHandler(
        IEspecialistaRepository especialistaRepository,
        IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> especialistaLocalAtendimentoRepository,
        IHttpContextAccessor httpContext)
    {
        _especialistaRepository = especialistaRepository;
        _especialistaLocalAtendimentoRepository = especialistaLocalAtendimentoRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(DeleteLocalAtendimentoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.EspecialistaId, ETipoPerfilAcesso.Profissional);

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var localAtendimento = especialista.LocaisAtendimento.FirstOrDefault(c => c.Id == request.LocalAtendimentoId);
        NotFoundException.ThrowIfNull(localAtendimento,
            $"Local de Atendimento com o ID: '{request.LocalAtendimentoId}' não encontrado para o Especialista com o ID: '{request.EspecialistaId}'");

        var rowsAffected = await _especialistaLocalAtendimentoRepository.RemoveAsync(localAtendimento);

        return await Task.FromResult(rowsAffected > 0);
    }
}