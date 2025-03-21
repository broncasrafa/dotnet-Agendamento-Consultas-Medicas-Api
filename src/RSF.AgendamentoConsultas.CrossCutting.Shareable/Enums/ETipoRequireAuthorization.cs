using System.ComponentModel;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

public enum ETipoRequireAuthorization
{
    [Description("None")]
    None = 0,

    [Description("AdminOrEspecialista")]
    AdminOrEspecialista = 1,

    [Description("AdminOrPaciente")]
    AdminOrPaciente = 2,

    [Description("AdminOrConsultor")]
    AdminOrConsultor = 3,

    [Description("AdminOrPacienteOrEspecialista")]
    AdminOrPacienteOrEspecialista = 4,

    [Description("PacienteOrEspecialista")]
    PacienteOrEspecialista = 5,

    [Description("OnlyEspecialistas")]
    OnlyEspecialistas = 6,

    [Description("OnlyPacientes")]
    OnlyPacientes = 7,

    [Description("OnlyAdmin")]
    OnlyAdmin = 8
}