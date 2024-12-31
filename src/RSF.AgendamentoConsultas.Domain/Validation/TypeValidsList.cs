namespace RSF.AgendamentoConsultas.Domain.Validation;

public static class TypeValids
{
    public static readonly string[] VALID_GENEROS = ["Masculino", "Feminino", "Não informado"];
    public static readonly string[] VALID_CATEGORIAS = ["Basic", "Premium"];
    public static readonly string[] VALID_TRATAMENTOS = ["Dr.", "Dra.", "Não informado"];
    public static readonly string[] VALID_ESPECIALIDADES = ["Principal", "SubEspecialidade"];
    public static readonly string[] VALID_PACIENTES = ["Principal", "Dependente"];
    public static readonly int[] VALID_SCORES = [1, 2, 3, 4, 5];
}