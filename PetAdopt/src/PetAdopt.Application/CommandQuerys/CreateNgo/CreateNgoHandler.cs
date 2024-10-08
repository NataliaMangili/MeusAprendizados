namespace PetAdopt.Application.CommandQuerys.CreateNgo;

public class CreateAdoptionHandler : IRequestHandler<CreateNgoCommand, bool>
{
    private readonly IUnitOfWorkRepository _unitOfWork;
    private readonly NgoMapper _mapper;
    private readonly INgoRepository _ngoRepository;
    private readonly ILogger<CreateNgoCommand> _logger;

    public CreateAdoptionHandler(IUnitOfWorkRepository unitOfWork, NgoMapper mapper, ILogger<CreateNgoCommand> logger, INgoRepository ngoRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ngoRepository = ngoRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateNgoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            Ngo newNgo = _mapper.Map(command.request);
            newNgo.AddAddress(command.request.address);
            newNgo.AddVolunteersContact(command.request.contacts);

            _ngoRepository.Add(newNgo);
            bool result = await _unitOfWork.CommitAsync();
            return result;
        }
        catch (ValidationExceptionErrorsBase ex)
        {
            throw ex;
        }
    }
}