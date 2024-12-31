namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Estado
{
    public int EstadoId { get; set; }
    public string Descricao { get; set; }
    public string Sigla { get; set; }
    public int RegiaoId { get; set; }

    public Regiao Regiao { get; set; }
    public ICollection<Cidade> Cidades { get; set; }
}