using RSF.AgendamentoConsultas.Domain.Validation;
using RSF.AgendamentoConsultas.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class PerguntaRespostaReacao
{
    public int PerguntaRespostaReacaoId { get; private set; }
    public int RespostaId { get; private set; }
    public int PacienteId { get; private set; }
    public ETipoReacaoResposta Reacao { get; private set; } = ETipoReacaoResposta.None;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public PerguntaResposta Resposta { get; private set; }

    protected PerguntaRespostaReacao()
    {
    }

    public PerguntaRespostaReacao(int respostaId, int pacienteId, ETipoReacaoResposta reacao)
    {
        RespostaId = respostaId;
        PacienteId = pacienteId;
        Reacao = reacao;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Update(ETipoReacaoResposta reacao)
    {
        Reacao = reacao;

        Validate();
    }

    public static ETipoReacaoResposta ConverterReacaoParaEnum(string reacao)
    {
        if (string.IsNullOrWhiteSpace(reacao))
            return ETipoReacaoResposta.None;

        // Converte a string para lowercase e compara com os valores do enum
        var reacaoLower = reacao.Trim().ToLower();

        foreach (ETipoReacaoResposta tipo in Enum.GetValues<ETipoReacaoResposta>())
        {
            if (tipo.ToString().ToLower().Equals(reacaoLower))
                return tipo;
        }

        return ETipoReacaoResposta.None; // Retorna None se não encontrar correspondência
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(RespostaId, nameof(RespostaId));
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        //if (!string.IsNullOrWhiteSpace(Reacao)) DomainValidation.PossiblesValidTypes(TypeValids.VALID_REACOES, value: Reacao, nameof(Reacao));        
    }
}