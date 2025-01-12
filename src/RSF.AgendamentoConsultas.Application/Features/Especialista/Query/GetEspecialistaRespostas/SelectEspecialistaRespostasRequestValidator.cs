using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Query.GetEspecialistaRespostas;

public class SelectEspecialistaRespostasRequestValidator : AbstractValidator<SelectEspecialistaRespostasRequest>
{
    public SelectEspecialistaRespostasRequestValidator()
    {
        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");
    }
}