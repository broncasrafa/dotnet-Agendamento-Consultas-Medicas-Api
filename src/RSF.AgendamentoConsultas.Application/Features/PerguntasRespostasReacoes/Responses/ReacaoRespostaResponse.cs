using RSF.AgendamentoConsultas.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Responses;

public class ReacaoRespostaResponse
{
    public int Id { get; set; }
    public int RespostaId { get; set; }
    public int PacienteId { get; set; }
    public ETipoReacaoResposta Reacao { get; set; } = ETipoReacaoResposta.None;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

