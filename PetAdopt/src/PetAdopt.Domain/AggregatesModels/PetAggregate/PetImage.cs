namespace PetAdopt.Domain.AggregatesModels.PetAggregate;
public class PetImage : BaseModel
{
    public string Link { get; set; }

    public static PetImage NewLink(string value) => new PetImage { Link = value };

}
