namespace PetAdopt.Domain.Aggregates.NgoAggregate;
public class Ngo : BaseModel
{
    public Ngo(Guid mainResponsible, string apresentation, string history, DateTime creationDate)
    {
        MainResponsible = mainResponsible;
        Apresentation = apresentation;
        History = history;
        CreationDate = creationDate;
    }

    public Guid MainResponsible { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Code { get; private set; }
    public string Apresentation { get; private set; }
    public string History { get; private set; }
    public DateTime CreationDate { get; private set; }

    //Ter um get all de ongs
    public void ValidateOng()
    {
        if (string.IsNullOrWhiteSpace(Apresentation))
            throw new InvalidOperationException("The NGO must have a apresentation.");

        if (CreationDate == default(DateTime))
            throw new InvalidOperationException("The NGO must have a valid creation date.");
    }


    // Lógica de negócios: Atualizar o responsável principal
    public void UpdateMainResponsible(Guid newResponsible) => MainResponsible = newResponsible;

    // Lógica de negócios: Atualizar o resumo da ONG
    public void UpdateApresentation(string newApresentation)
    {
        if (string.IsNullOrWhiteSpace(newApresentation))
            throw new ArgumentException("New Apresentation cannot be null or empty.");

        Apresentation = newApresentation;
    }

}
