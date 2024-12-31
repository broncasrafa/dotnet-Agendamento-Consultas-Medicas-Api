namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Regiao
{
    public int RegiaoId { get; set; }
    public string Descricao { get; set; }

    public ICollection<Estado> Estados { get; set; }
}