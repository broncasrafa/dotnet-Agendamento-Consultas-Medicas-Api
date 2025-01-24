using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Query.GetAuthenticatedUserInfo;

public class SelectAuthenticatedUserInfoRequestValidator : AbstractValidator<SelectAuthenticatedUserInfoRequest>
{
    public SelectAuthenticatedUserInfoRequestValidator()
    {        
    }
}