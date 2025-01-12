﻿using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.UpdatePacientePlanoMedico;

public record UpdatePacientePlanoMedicoRequest(
    int PacienteId,
    int PlanoMedicoId,
    string NomePlano,
    string NumeroCarteirinha) : IRequest<Result<bool>>;