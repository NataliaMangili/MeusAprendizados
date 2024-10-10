namespace PetAdopt.Application.Events;

public abstract record EventDTOBase
{
    public EventDTOBase()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public Guid Id { get; }

    public DateTime CreationDate { get; }
}
