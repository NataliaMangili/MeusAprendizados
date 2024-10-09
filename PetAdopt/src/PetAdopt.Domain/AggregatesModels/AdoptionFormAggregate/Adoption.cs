namespace PetAdopt.Domain.AggregatesModels.AdoptionFormAggregate;

public class Adoption : BaseModel
{
    public Adoption(Guid petId, Guid adopterId, DateTime adoptionDate)
    {
        PetId = petId;
        AdopterId = adopterId;
    }

    [ForeignKey("Pet")]
    public Guid PetId { get; set; }
    public virtual Pet Pet { get; set; }

    public Guid AdopterId { get; private set; }

    public DateTime AdoptionDate { get; private set; } = DateTime.UtcNow;
}
