﻿using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;

public record RegisterRequest(
    string NomeCompleto,
    string CPF,
    string Username,
    string Email,
    string Telefone,
    string Genero,
    string TipoAcesso,
    string Password,
    string ConfirmPassword) : IRequest<Result<AuthenticatedUserResponse>>;



