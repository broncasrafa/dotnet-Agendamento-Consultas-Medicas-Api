namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class Cidade
{    
    public int CidadeId { get; set; }
    public string Descricao { get; set; }

    /// <summary>
    /// Código da cidade. 
    /// De: Espigão D'oeste - RO 
    /// Para: espigao-d-oeste-ro
    /// </summary>
    public string Code { get; set; }
    public int EstadoId { get; set; }

    public Estado Estado { get; set; }
}