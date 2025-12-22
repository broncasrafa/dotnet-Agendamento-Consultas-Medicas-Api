using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CreateAgendamento;

public class CreateAgendamentoRequestValidator : AbstractValidator<CreateAgendamentoRequest>
{
    public CreateAgendamentoRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.EspecialidadeId).IdValidators("Especialidade");
        RuleFor(x => x.LocalAtendimentoId).IdValidators("Local de atendimento");
        RuleFor(x => x.TipoConsultaId).IdValidators("Tipo de Consulta");
        RuleFor(x => x.TipoAgendamentoId).IdValidators("Tipo de Agendamento");

        RuleFor(x => x.DataConsulta)
            .NotEmpty()
                .WithMessage("A data da consulta é obrigatória, não pode ser nula ou vazia")
            .Must(data => data.Date > DateTime.Today)
                .WithMessage("A data da consulta deve ser maior que a data atual")
            .Must(data => data.ToString("yyyy-MM-dd") == data.Date.ToString("yyyy-MM-dd"))
                .WithMessage("A data da consulta deve estar no formato 'yyyy-MM-dd'");

        RuleFor(x => x.HorarioConsulta)
            .NotNullOrEmptyValidators("horário da consulta")
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d$")
                .WithMessage("O horário da consulta deve estar no formato 'HH:mm' e ser um horário válido.");

        RuleFor(c => c.MotivoConsulta).NotNullOrEmptyValidators("Motivo da consulta", minLength: 5);

        RuleFor(c => c.ValorConsulta)
            .ValorMonetarioValidations(field: "valor da consulta")
            .When(c => c.TipoAgendamentoId == (int)ETipoAgendamento.Particular);

        RuleFor(c => c.TelefoneContato).TelefoneValidators();

        RuleFor(x => x.PacienteId).IdValidators("Paciente");

        RuleFor(x => x.PlanoMedicoId).IdValidators("Plano Medico do Paciente/Dependente", c => c.TipoAgendamentoId == (int)ETipoAgendamento.Convenio);
            //.GreaterThan(0).When(c => c.TipoAgendamentoId == (int)ETipoAgendamento.Convenio)
            //.WithMessage("O ID do Plano Medico do Paciente/Dependente deve ser maior que 0");

        RuleFor(x => x.DependenteId).IdValidators("Dependente", c => c.DependenteId.HasValue);
            //.GreaterThan(0).When(c => c.DependenteId.HasValue)
            //.WithMessage("O ID do Dependente deve ser maior que 0");
    }
}