using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetAll;

public class SelectCidadesRequestValidator : AbstractValidator<SelectCidadesRequest>
{
    public SelectCidadesRequestValidator()
    {        
    }
}