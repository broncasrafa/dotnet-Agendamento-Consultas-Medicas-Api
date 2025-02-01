namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

public enum ETipoStatusConsulta
{
    /// <summary>
    /// Status default para novas consultas
    /// </summary>
    Solicitado = 1,
    /// <summary>
    /// Consulta confirmada e pronta para ser atendida
    /// </summary>
    Confirmado = 2,
    /// <summary>
    /// Status de cancelamento por qualquer motivo exceto respostas de especialistas e respostas de pacientes
    /// </summary>
    Cancelado = 3,
    /// <summary>
    /// Consulta cancelada automaticamente, quando não recebemos a resposta do especialista em tempo hábil.
    /// </summary>
    ExpiradoProfissional = 4,
    /// <summary>
    /// Consulta cancelada automaticamente, quando não recebemos a resposta do paciente em tempo hábil para confirmação ou remarcação de consulta.
    /// </summary>
    ExpiradoPaciente = 5,
    /// <summary>
    /// Consulta atendida pelo especialista
    /// </summary>
    Atendido = 6
}