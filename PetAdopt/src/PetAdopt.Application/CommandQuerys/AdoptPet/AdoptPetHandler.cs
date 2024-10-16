using KafkaEventBus.Interfaces;
using PetAdopt.Application.Events.DTOs;
using PetAdopt.Domain.AggregatesModels.AdoptionAggregate;
using PetAdopt.Domain.AggregatesModels.AdoptionFormAggregate;
using PetAdopt.Domain.Enums;

namespace PetAdopt.Application.CommandQuerys.AdoptPet;

public class AdoptPetHandler : IRequestHandler<AdoptPetCommand, Result>
{
    private readonly IUnitOfWorkRepository _unitOfWork;
    private readonly IRepositoryBase _baseRepository;
    private readonly ILogger<AdoptPetHandler> _logger;
    private readonly IEventProducer _eventProducer;

    public AdoptPetHandler(IUnitOfWorkRepository unitOfWork, ILogger<AdoptPetHandler> logger, IRepositoryBase baseRepository, IEventProducer eventProducer)
    {
        _unitOfWork = unitOfWork;
        _baseRepository = baseRepository;
        _logger = logger;
        _eventProducer = eventProducer;
    }
    //TODO Teste de status Pet
    public async Task<Result> Handle(AdoptPetCommand command, CancellationToken cancellationToken)
    {
        // Iniciar uma transação/ no escopo -> using( var transaction = ... ?
        await _unitOfWork.BeginTransactionAsync();
        {
            try
            {
                var adoptionForm = await _baseRepository.GetAsync<AdoptionForm>(command.request.AdoptionFormId);

                // Verifica se o Pet existe e se está disponível para adoção
                var pet = await _baseRepository.GetAsync<Pet>(adoptionForm.PetId);
                if (pet == null || pet.StatusPet != StatusPetEnum.PendingAdoption)
                {
                    return Result.Failure("Pet not available for adoption");
                }

                // Cria o registro de adoção
                var adoption = new Adoption(adoptionForm.PetId, adoptionForm.AdopterId);
                await _baseRepository.AddAsync(adoption);

                // Troca o Status do Pet para adotado
                pet.StatusPet = StatusPetEnum.Adopted;
                await _baseRepository.UpdateAsync(pet);

                // Salvar as mudanças no banco de dados e confirmar a transação
                await _unitOfWork.CommitTransactionAsync();

                // Mensageria TODO
                var eventMessage = new PetAdoptedSendEmailEvent
                {
                    Contact = adoptionForm.AdopterContact,
                    Name = adoptionForm.AdopterName,
                    PetName = pet.Name,
                    About = pet.About,
                    IsNeutered = pet.IsNeutered,
                };

                await _eventProducer.ProduceAsync("pet-adopted-topic", eventMessage);

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
