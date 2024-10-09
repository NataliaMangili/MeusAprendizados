using MediatR;

namespace PetAdopt.Application.CommandQuerys.AdoptPet;

public record AdoptPetCommand(AdoptPetDTO request) : IRequest<Result>;
