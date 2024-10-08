using MediatR;
using PetAdopt.Application.DTO;

namespace PetAdopt.Application.CommandQuerys.CreateNgo;

public record CreateNgoCommand(CreateNgoRequest request) : IRequest<bool>;
