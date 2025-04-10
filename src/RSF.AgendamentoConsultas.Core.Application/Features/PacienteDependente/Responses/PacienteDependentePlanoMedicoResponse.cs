﻿namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;

public record PacienteDependentePlanoMedicoResponse
(
    int Id,
    string NomePlano,
    string NumCartao,
    int? ConvenioMedicoId,
    string ConvenioMedico,
    bool Ativo
)
{
    public static PacienteDependentePlanoMedicoResponse MapFromEntity(Domain.Entities.PacienteDependentePlanoMedico entity)
        => entity is null ? default! : new PacienteDependentePlanoMedicoResponse(
            entity.PlanoMedicoId,
            entity.NomePlano,
            entity.NumCartao,
            entity.ConvenioMedico?.ConvenioMedicoId,
            entity.ConvenioMedico?.Nome,
            entity.Ativo ?? false);

    public static IReadOnlyList<PacienteDependentePlanoMedicoResponse>? MapFromEntity(IEnumerable<Domain.Entities.PacienteDependentePlanoMedico> collection)
        => collection is null || !collection.Any() ? default! : collection.Where(c => c.Ativo!.Value).Select(c => MapFromEntity(c)).ToList();
}