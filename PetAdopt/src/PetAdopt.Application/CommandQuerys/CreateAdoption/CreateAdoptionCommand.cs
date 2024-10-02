using MediatR;

namespace PetAdopt.Application.CommandQuerys.CreateAdoption;

public record CreateAdoptionCommand(string user) : IRequest<bool>;
