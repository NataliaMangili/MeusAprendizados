namespace PetAdopt.Domain.Aggregates.NgoAggregate;
public class Ngo : BaseModel
{
    public Ngo(Guid mainResponsibleId, string mainResponsibleName, string apresentation, string history, DateTime creationDate)
    {
        MainResponsibleId = mainResponsibleId;
        MainResponsibleName = mainResponsibleName;
        Apresentation = apresentation;
        History = history;
        CreationDate = creationDate;
    }

    public Guid MainResponsibleId { get; set; }
    public string MainResponsibleName { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Code { get; set; }
    public string Apresentation { get; set; }
    public string History { get; set; }
    public DateTime CreationDate { get; set; }

    //Ter um get all de ongs
    public void ValidateOng()
    {
        if (string.IsNullOrWhiteSpace(Apresentation))
            throw new InvalidOperationException("The NGO must have a apresentation.");

        if (CreationDate == default(DateTime))
            throw new InvalidOperationException("The NGO must have a valid creation date.");
    }


    // Lógica de negócios: Atualizar o responsável principal
    public void UpdateMainResponsible(Guid newResponsible) => MainResponsibleId = newResponsible;

    // Lógica de negócios: Atualizar o resumo da ONG
    public void UpdateApresentation(string newApresentation)
    {
        if (string.IsNullOrWhiteSpace(newApresentation))
            throw new ArgumentException("New Apresentation cannot be null or empty.");

        Apresentation = newApresentation;
    }

}
