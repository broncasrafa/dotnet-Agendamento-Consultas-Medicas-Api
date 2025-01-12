﻿using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePacienteAgendamento;

public record DeletePacienteAgendamentoRequest(
    int PacienteId,
    int AgendamentoId,
    string NotaCancelamento
    ) : IRequest<Result<bool>>;