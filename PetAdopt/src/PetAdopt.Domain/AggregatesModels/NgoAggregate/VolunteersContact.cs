namespace PetAdopt.Domain.Aggregates.NgoAggregate;

public class VolunteersContact : BaseModel
{
    // Construtor sem parâmetros para o EF Core
    private VolunteersContact() { }

    public VolunteersContact(ContactVO contactVO)
    {
        this.contactVO = contactVO;
    }


    [ForeignKey("Ngo")]
    public Guid NgoId { get; set; }
    public virtual Ngo Ngo { get; set; }

    public ContactVO contactVO { get; set; }
}
