using PetAdopt.Domain.ValueObjects;

namespace PetAdopt.Application.DTO;

public record CreateNgoRequest(
    Guid MainResponsibleId,
    string MainResponsibleName,
    string Apresentation,
    string History,
    DateTime CreationDate,
    List<AddressVO> address,
    List<ContactVO> contacts);
