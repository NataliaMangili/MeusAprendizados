namespace PetAdopt.Domain.AggregatesModels.NgoAggregate;

public class NgoAddress : BaseModel
{
    // Construtor sem parâmetros para o EF Core
    private NgoAddress() { }

    public NgoAddress(AddressVO addressVO)
    {
        this.addressVO = addressVO;
    }

    [ForeignKey("Ngo")]
    public Guid NgoId { get; set; }
    public virtual Ngo Ngo { get; set; }

    public AddressVO addressVO { get; set; }
}
