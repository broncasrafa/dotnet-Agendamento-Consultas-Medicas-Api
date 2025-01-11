namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

public class PacienteResultList<T>
{
    public int PacienteId { get; private set; }
    public IReadOnlyList<T> Results { get; private set; }

    public PacienteResultList(int pacienteId, IReadOnlyList<T> results)
    {
        PacienteId = pacienteId;
        Results = results ?? [];
    }
}