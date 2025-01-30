using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddLocalAtendimento;

public class AddLocalAtendimentoRequestHandler : IRequestHandler<AddLocalAtendimentoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> _especialistaLocalAtendimentoRepository;
    private readonly IHttpContextAccessor _httpContext;

    public AddLocalAtendimentoRequestHandler(
        IEspecialistaRepository especialistaRepository,
        IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> especialistaLocalAtendimentoRepository,
        IHttpContextAccessor httpContext)
    {
        _especialistaRepository = especialistaRepository;
        _especialistaLocalAtendimentoRepository = especialistaLocalAtendimentoRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(AddLocalAtendimentoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.EspecialistaId, ETipoPerfilAcesso.Profissional);

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var localAtendimento = new Domain.Entities.EspecialistaLocalAtendimento(
            especialistaId: request.EspecialistaId,
            nome: request.Nome,
            logradouro: request.Logradouro,
            complemento: request.Complemento,
            bairro: request.Bairro,
            cep: request.Cep,
            cidade: request.Cidade,
            estado: request.Estado,
            preco: request.Preco,
            tipoAtendimento: request.TipoAtendimento,
            telefone: request.Telefone,
            whatsapp: request.Whatsapp);

        especialista.LocaisAtendimento.Add(localAtendimento);

        var rowsAffected = await _especialistaLocalAtendimentoRepository.AddAsync(localAtendimento);
        
        return await Task.FromResult(rowsAffected > 0);
    }
}

