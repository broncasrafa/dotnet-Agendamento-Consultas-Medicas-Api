using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddLocalAtendimento
{
    public class AddLocalAtendimentoRequestValidator : AbstractValidator<AddLocalAtendimentoRequest>
    {
        public AddLocalAtendimentoRequestValidator()
        {
            RuleFor(x => x.EspecialistaId).IdValidators("Especialista");

            RuleFor(c => c.Nome).NotNullOrEmptyValidators("Nome do Local de Atendimento", minLength: 6, maxLength: 200);

            RuleFor(x => x.Logradouro)
                .NotNullOrEmptyValidators(
                    field:"Logradouro", 
                    minLength: 5, 
                    maxLength: 255, 
                    predicate: x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                               !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase), 
                    message: "O Logradouro é obrigatório, exceto para Teleconsulta ou Telemedicine");

            RuleFor(x => x.Complemento)
                .NotNullOrEmptyValidators("Complemento", minLength: 3, maxLength: 50, predicate: x => !string.IsNullOrWhiteSpace(x.Complemento));

            RuleFor(x => x.Bairro)
                .NotNullOrEmptyValidators(
                    field: "Bairro", 
                    minLength: 5, 
                    maxLength: 100,
                    predicate: x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                               !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase),
                    message: "O Bairro é obrigatório, exceto para Teleconsulta ou Telemedicine");

            RuleFor(x => x.Cep)
                .NotEmpty()
                .Length(8).WithMessage("O CEP deve ter exatamente 8 dígitos")
                .Matches(@"^\d{8}$").WithMessage("O CEP deve conter apenas números")
                    .When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                               !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                            .WithMessage("O CEP é obrigatório, exceto para Teleconsulta ou Telemedicine");

            //RuleFor(x => x.Cidade)
            //    .NotEmpty()
            //    .MinimumLength(3).WithMessage("O Cidade deve ter pelo menos 3 caracteres")
            //    .MaximumLength(100).WithMessage("O Cidade deve ter no máximo 100 caracteres")
            //        .When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
            //                   !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
            //                .WithMessage("O Cidade é obrigatório, exceto para Teleconsulta ou Telemedicine");
            RuleFor(x => x.Cidade)
                .NotNullOrEmptyValidators(
                    field: "Cidade",
                    minLength: 3,
                    maxLength: 100,
                    predicate: x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                               !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase),
                    message: "A Cidade é obrigatório, exceto para Teleconsulta ou Telemedicine"
                );

            RuleFor(x => x.Estado)
                .UfValidations().When(x => !string.Equals(x.Nome, "Teleconsulta", StringComparison.OrdinalIgnoreCase) &&
                                           !string.Equals(x.Nome, "Telemedicine", StringComparison.OrdinalIgnoreCase))
                            .WithMessage("O Estado é obrigatório, exceto para Teleconsulta ou Telemedicine");

            RuleFor(x => x.Preco)
                .ValorMonetarioValidations(field: "Preco");            

            RuleFor(c => c.TipoAtendimento)
                .NotNullOrEmptyValidators("Tipo de Atendimento", minLength: 6, maxLength: 200, predicate: x => !string.IsNullOrWhiteSpace(x.TipoAtendimento));

            RuleFor(x => x.Telefone)
                .NotNullOrEmptyValidators(field:"O Telefone é obrigatório, não deve ser nulo ou vazio")
                .Matches(@"^(\d+;?)*$").WithMessage("O Telefone deve conter apenas números e ponto-e-vírgula (';') para separar vários números de telefone");

            RuleFor(x => x.Whatsapp)
                .TelefoneValidators();
        }
    }
}
