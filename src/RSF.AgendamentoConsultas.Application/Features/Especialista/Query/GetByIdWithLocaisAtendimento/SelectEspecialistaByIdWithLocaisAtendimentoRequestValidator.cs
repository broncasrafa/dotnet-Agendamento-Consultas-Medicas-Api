using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Query.GetByIdWithLocaisAtendimento;

public class SelectEspecialistaByIdWithLocaisAtendimentoRequestValidator : AbstractValidator<SelectEspecialistaByIdWithLocaisAtendimentoRequest>
{
    public SelectEspecialistaByIdWithLocaisAtendimentoRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}