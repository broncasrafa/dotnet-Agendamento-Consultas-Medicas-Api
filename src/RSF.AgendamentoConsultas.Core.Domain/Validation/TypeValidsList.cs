using System.Diagnostics.CodeAnalysis;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Domain.Validation;

[ExcludeFromCodeCoverage]
public static class TypeValids
{
    public static readonly string[] VALID_GENEROS = ["Masculino", "Feminino", "Não informado"];
    public static readonly string[] VALID_CATEGORIAS = ["Basic", "Premium"];
    public static readonly string[] VALID_TRATAMENTOS = ["Dr.", "Dra.", "Não informado"];
    public static readonly string[] VALID_ESPECIALIDADES = ["Principal", "SubEspecialidade"];
    public static readonly string[] VALID_PACIENTES = ["Principal", "Dependente"];
    public static readonly int[] VALID_SCORES = [1, 2, 3, 4, 5];
    public static readonly int[] VALID_TIPO_AGENDAMENTOS = [(int)ETipoAgendamento.Particular, (int)ETipoAgendamento.Convenio];
    public static readonly int[] VALID_TIPO_CONSULTAS = [(int)ETipoConsulta.Presencial, (int)ETipoConsulta.Teleconsulta];
    public static readonly int[] VALID_TIPO_STATUS_CONSULTAS = [
        (int)ETipoStatusConsulta.Solicitado, 
        (int)ETipoStatusConsulta.Confirmado,
        (int)ETipoStatusConsulta.Cancelado,
        (int)ETipoStatusConsulta.ExpiradoProfissional,
        (int)ETipoStatusConsulta.ExpiradoPaciente,
    ];
    public static readonly string[] VALID_REACOES = ["Like", "Dislike"];
    public static readonly int[] VALID_PAGE_SIZES = [5, 10, 20, 50, 100];
}