using PetAdopt.Domain.AggregatesModels.AdoptionFormAggregate;
using PetAdopt.Domain.Enums;

namespace PetAdopt.Application.CommandQuerys.AdoptPet;

public class AdoptPetHandler : IRequestHandler<AdoptPetCommand, Result>
{
    private readonly IUnitOfWorkRepository _unitOfWork;
    private readonly IRepositoryBase _baseRepository;
    private readonly ILogger<AdoptPetHandler> _logger;

    public AdoptPetHandler(IUnitOfWorkRepository unitOfWork, ILogger<AdoptPetHandler> logger, IRepositoryBase baseRepository)
    {
        _unitOfWork = unitOfWork;
        _baseRepository = baseRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(AdoptPetCommand command, CancellationToken cancellationToken)
    {
        // Iniciar uma transação/ local - escopo using ?
        await _unitOfWork.BeginTransactionAsync();
        {
            try
            {
                //TODO Teste de status Pet

                // Verifica se o Pet existe e se está disponível para adoção
                var pet = await _baseRepository.GetAsync<Pet>(command.request.PetId);
                if (pet == null || pet.StatusPet != StatusPetEnum.PendingAdoption)
                {
                    return Result.Failure("Pet not available for adoption");
                }

                // Cria o registro de adoção
                var adoption = new Adoption(command.request.PetId, command.request.AdopterId);
                await _baseRepository.AddAsync(adoption);

                // Troca o Status do Pet para adotado
                pet.StatusPet = StatusPetEnum.Adopted;
                await _baseRepository.UpdateAsync(pet);

                // TODO: Futuramente Registrar a ação no log

                // Salvar as mudanças no banco de dados e confirmar a transação
                await _unitOfWork.CommitTransactionAsync();

                // Mensageria TODO AQUI

                // Retornar sucesso
                return Result.SuccessResult();
            }
            catch (Exception ex)
            {
                // Em caso de erro, desfaz todas as operações do UoW
                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(ex, "Erro ao tentar processar a adoção.");
                return Result.Failure("Erro ao processar a adoção. Tente novamente mais tarde.");
            }
        }
    }
}
