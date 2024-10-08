using PetAdopt.Domain.AggregatesModels.NgoAggregate;
using System.Collections.Generic;

namespace PetAdopt.Domain.Aggregates.NgoAggregate;
public class Ngo : BaseModel
{
    private readonly List<NgoAddress> _ngoAddress;
    public virtual IReadOnlyCollection<NgoAddress> NgoAddresses => _ngoAddress.AsReadOnly();

    private readonly List<VolunteersContact> _volunteersContact;
    public virtual IReadOnlyCollection<VolunteersContact> VolunteersContacts => _volunteersContact.AsReadOnly();

    public Ngo()
    {
        _ngoAddress = new List<NgoAddress>();
        _volunteersContact = new List<VolunteersContact>();
    }

    public Ngo(Guid mainResponsibleId, string mainResponsibleName, string apresentation, string history, DateTime creationDate) : this()
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


    public void AddAddress(List<AddressVO> addr) => _ngoAddress.AddRange(addr.Select(x => new NgoAddress(x)));
    public void AddVolunteersContact(List<ContactVO> vc) => _volunteersContact.AddRange(vc.Select(x => new VolunteersContact(x)));

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
