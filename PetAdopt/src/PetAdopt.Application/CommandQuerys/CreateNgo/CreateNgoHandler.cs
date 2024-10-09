using System.Threading.Channels;

namespace PetAdopt.Application.CommandQuerys.CreateNgo;

public class CreateAdoptionHandler : IRequestHandler<CreateNgoCommand, Result>
{
    private readonly IUnitOfWorkRepository _unitOfWork;
    private readonly NgoMapper _mapper;
    private readonly INgoRepository _ngoRepository;
    private readonly ILogger<CreateAdoptionHandler> _logger;

    public CreateAdoptionHandler(IUnitOfWorkRepository unitOfWork, NgoMapper mapper, ILogger<CreateAdoptionHandler> logger, INgoRepository ngoRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ngoRepository = ngoRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateNgoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            Ngo newNgo = _mapper.Map(command.request);
            newNgo.AddAddress(command.request.address);
            newNgo.AddVolunteersContact(command.request.contacts);

            _ngoRepository.Add(newNgo);

            int result = await _unitOfWork.CommitAsync();
            if (result > 2) return Result.SuccessResult();

            return Result.Failure("No changes were detected.");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while attempting to process the command.");
            throw new Exception("Unexpected error occurred while attempting to save Ngo.");
        }
    }
}