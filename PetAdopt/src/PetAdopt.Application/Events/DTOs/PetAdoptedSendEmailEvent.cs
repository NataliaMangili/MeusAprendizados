namespace PetAdopt.Application.Events.DTOs;

public record PetAdoptedSendEmailEvent : EventDTOBase, INotification
{
    public string PetName { get; set; }
    public string About { get; set; }
    public bool IsNeutered { get; set; }
    public string Name { get; set; }
    public string Contact { get; set; }
}