using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateLocalAtendimento;

public class UpdateLocalAtendimentoRequestHandler : IRequestHandler<UpdateLocalAtendimentoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> _especialistaLocalAtendimentoRepository;

    public UpdateLocalAtendimentoRequestHandler(
        IEspecialistaRepository especialistaRepository, 
        IBaseRepository<Domain.Entities.EspecialistaLocalAtendimento> especialistaLocalAtendimentoRepository)
    {
        _especialistaRepository = especialistaRepository;
        _especialistaLocalAtendimentoRepository = especialistaLocalAtendimentoRepository;
    }

    public async Task<Result<bool>> Handle(UpdateLocalAtendimentoRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var localAtendimento = especialista.LocaisAtendimento.FirstOrDefault(c => c.Id == request.Id);
        NotFoundException.ThrowIfNull(localAtendimento,
            $"Local de Atendimento com o ID de registro: '{request.Id}' não encontrado para o Especialista com o ID: '{request.EspecialistaId}'");

        localAtendimento!.Update(
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

        var rowsAffected = await _especialistaLocalAtendimentoRepository.UpdateAsync(localAtendimento);

        return await Task.FromResult(rowsAffected > 0);
    }
}