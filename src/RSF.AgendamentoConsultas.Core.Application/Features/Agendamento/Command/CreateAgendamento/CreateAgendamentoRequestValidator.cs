using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CreateAgendamento;

public class CreateAgendamentoRequestValidator : AbstractValidator<CreateAgendamentoRequest>
{
    public CreateAgendamentoRequestValidator()
    {
        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(x => x.EspecialidadeId)
            .GreaterThan(0)
            .WithMessage("O ID da Especialidade deve ser maior que 0");

        RuleFor(x => x.LocalAtendimentoId)
            .GreaterThan(0)
            .WithMessage("O ID do Local de atendimento deve ser maior que 0");

        RuleFor(x => x.TipoConsultaId)
            .GreaterThan(0)
            .WithMessage("O ID do Tipo de Consulta deve ser maior que 0");

        RuleFor(x => x.TipoAgendamentoId)
            .GreaterThan(0)
            .WithMessage("O ID do Tipo de Agendamento deve ser maior que 0");

        RuleFor(x => x.DataConsulta)
            .NotEmpty()
                .WithMessage("A data da consulta é obrigatória, não pode ser nula ou vazia")
            .Must(data => data.Date > DateTime.Today)
                .WithMessage("A data da consulta deve ser maior que a data atual")
            .Must(data => data.ToString("yyyy-MM-dd") == data.Date.ToString("yyyy-MM-dd"))
                .WithMessage("A data da consulta deve estar no formato 'yyyy-MM-dd'");

        RuleFor(x => x.HorarioConsulta)
            .NotEmpty()
                .WithMessage("O horário da consulta é obrigatório, não pode ser nulo ou vazio.")
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d$")
                .WithMessage("O horário da consulta deve estar no formato 'HH:mm' e ser um horário válido.");

        RuleFor(c => c.MotivoConsulta)
            .NotEmpty().WithMessage("O Motivo da consulta é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O Motivo da consulta deve ter pelo menos 5 caracteres");

        RuleFor(c => c.ValorConsulta)
            .ValorMonetarioValidations(field: "valor da consulta");

        RuleFor(c => c.TelefoneContato)
            .TelefoneValidators();

        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.PlanoMedicoId)
            .GreaterThan(0)
            .WithMessage("O ID do Plano Medico do Paciente/Dependente deve ser maior que 0");

        RuleFor(x => x.DependenteId)
            .GreaterThan(0).When(c => c.DependenteId.HasValue)
            .WithMessage("O ID do Dependente deve ser maior que 0");
    }
}