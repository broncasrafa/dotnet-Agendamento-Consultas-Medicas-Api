using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaTags
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int TagsId { get; private set; }

    public Especialista Especialista { get; set; }
    public Tags Tag { get; set; }

    public EspecialistaTags(int especialistaId, int tagsId)
    {
        EspecialistaId = especialistaId;
        TagsId = tagsId;

        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(tagsId, nameof(TagsId));
    }
}