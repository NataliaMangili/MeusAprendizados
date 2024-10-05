using MediatR;
using Microsoft.Extensions.Logging;
using PetAdopt.Application.Exceptions;
using PetAdopt.Domain.Interfaces;
using PetAdopt.Domain.AggregatesModels;

namespace PetAdopt.Application.CommandQuerys.CreateAdoption;

public class CreateAdoptionHandler : IRequestHandler<CreateAdoptionCommand, bool>
{
    //private readonly AdoptionMapper _mapper;
    private readonly IRepositoryBase _baseRepository;
    private readonly ILogger<CreateAdoptionCommand> _logger;

    public CreateAdoptionHandler(/*AdoptionMapper mapper,*/ ILogger<CreateAdoptionCommand> logger, IRepositoryBase baseRepository)
    {
        //_mapper = mapper;
        _baseRepository = baseRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateAdoptionCommand command, CancellationToken cancellationToken)
    {
        //var validator = new CreateAdoptionRequestVA(_logger);
        try
        {
            //AdoptionForm adoption = new AdoptionForm() { Name = "AdoptTeste" };
            var result = await _baseRepository.DatabaseSaveChanges();
            return result;
        }
        catch (ValidationExceptionErrorsBase ex)
        {
            throw ex;
        }
    }
}