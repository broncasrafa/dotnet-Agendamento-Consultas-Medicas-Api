﻿using RSF.AgendamentoConsultas.Core.Domain.Validation;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByFilterPaged;

public class SelectEspecialistaByFiltersPagedRequestValidator : AbstractValidator<SelectEspecialistaByFiltersPagedRequest>
{
    public SelectEspecialistaByFiltersPagedRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .Must(pageSize => TypeValids.VALID_PAGE_SIZES.Contains(pageSize)).WithMessage("PageSize deve ser 5, 10, 20, 50 ou 100.");

        RuleFor(x => x.PageNum)
            .GreaterThan(0).WithMessage("PageNum deve ser que zero.");

        RuleFor(x => x.Cidade)
            .NotEmpty().WithMessage("O nome da Cidade não pode ser nulo ou vazio.")
            .MinimumLength(2).WithMessage("O nome da Cidade deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome da Cidade deve conter apenas letras e espaços.");
    }
}