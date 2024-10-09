namespace PetAdopt.Application.CommandQuerys.CreateNgo;

public record CreateNgoCommand(CreateNgoRequest request) : IRequest<Result>;
