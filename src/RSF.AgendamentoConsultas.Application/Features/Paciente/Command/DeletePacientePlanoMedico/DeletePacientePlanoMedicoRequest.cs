﻿using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePacientePlanoMedico;

public record DeletePacientePlanoMedicoRequest(int PacienteId, int PlanoMedicoId) : IRequest<Result<bool>>;