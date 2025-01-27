using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateLocalAtendimento;

public class UpdateLocalAtendimentoRequestValidator : AbstractValidator<UpdateLocalAtendimentoRequest>
{
    public UpdateLocalAtendimentoRequestValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("O ID do registro deve ser maior que 0");

        RuleFor(c => c.EspecialistaId)
                .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O Nome do Local de Atendimento é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(6).WithMessage("O Nome do Local de Atendimento deve ter no mínimo 6 caracteres")
            .MaximumLength(200).WithMessage("O Nome do Local de Atendimento deve ter no máximo 200 caracteres");

        RuleFor(x => x.Logradouro)
            .NotEmpty()
            .MinimumLength(5).WithMessage("O Logradouro deve ter pelo menos 5 caracteres")
            .MaximumLength(255).WithMessage("O Logradouro deve ter no máximo 255 caracteres")
                .When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                           !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                        .WithMessage("O Logradouro é obrigatório, exceto para Teleconsulta ou Telemedicine");

        RuleFor(x => x.Complemento)
            .MinimumLength(3).WithMessage("O Complemento deve ter pelo menos 3 caracteres")
            .MaximumLength(50).WithMessage("O Complemento deve ter no máximo 50 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Complemento));

        RuleFor(x => x.Bairro)
            .NotEmpty()
            .MinimumLength(5).WithMessage("O Bairro deve ter pelo menos 5 caracteres")
            .MaximumLength(100).WithMessage("O Bairro deve ter no máximo 100 caracteres")
                .When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                           !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                        .WithMessage("O Bairro é obrigatório, exceto para Teleconsulta ou Telemedicine");

        RuleFor(x => x.Cep)
            .NotEmpty()
            .Length(8).WithMessage("O CEP deve ter exatamente 8 dígitos")
            .Matches(@"^\d{8}$").WithMessage("O CEP deve conter apenas números")
                .When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                           !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                        .WithMessage("O CEP é obrigatório, exceto para Teleconsulta ou Telemedicine");

        RuleFor(x => x.Cidade)
            .NotEmpty()
            .MinimumLength(3).WithMessage("O Cidade deve ter pelo menos 3 caracteres")
            .MaximumLength(100).WithMessage("O Cidade deve ter no máximo 100 caracteres")
                .When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                           !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                        .WithMessage("O Cidade é obrigatório, exceto para Teleconsulta ou Telemedicine");

        RuleFor(x => x.Estado)
            .UfValidations().When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                                       !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                        .WithMessage("O Estado é obrigatório, exceto para Teleconsulta ou Telemedicine");

        RuleFor(x => x.Preco)
            .ValorMonetarioValidations(field: "Preco");

        RuleFor(c => c.TipoAtendimento)
            .MinimumLength(6).WithMessage("O Tipo de Atendimento deve ter no mínimo 6 caracteres")
            .MaximumLength(200).WithMessage("O Tipo de Atendimento deve ter no máximo 200 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.TipoAtendimento));

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O Telefone é obrigatório, não deve ser nulo ou vazio")
            .Matches(@"^(\d+;?)*$").WithMessage("O Telefone deve conter apenas números e ponto-e-vírgula (';') para separar vários números de telefone");

        RuleFor(x => x.Whatsapp)
            .TelefoneValidators();
    }
}