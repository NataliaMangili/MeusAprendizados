using PetAdopt.Application.Events.DTOs;

namespace PetAdopt.Application.Events.Handlers;

public class PetAdoptedSendEmailEventHandler : INotificationHandler<PetAdoptedSendEmailEvent>
{
    //private readonly IEmailService _emailService; // Serviço de envio de e-mail
    private readonly ILogger<PetAdoptedSendEmailEventHandler> _logger;

    public PetAdoptedSendEmailEventHandler(/*IEmailService emailService,*/ ILogger<PetAdoptedSendEmailEventHandler> logger)
    {
        //_emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(PetAdoptedSendEmailEvent petAdoptedEvent, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"Tratando evento: {petAdoptedEvent.PetName}");
            // Template Email aqui
            var subject = "Congratulations on Your New Pet!";
            var body = $"You have successfully adopted pet with ID: {petAdoptedEvent.PetName}. " +
                       $"Thank you for giving a pet a new home!";

            // Envio do e-mail
            //await _emailService.SendEmailAsync(petAdoptedEvent.AdopterId.ToString(), subject, body);

            _logger.LogInformation($"E-mail enviado para o adotante {petAdoptedEvent.Name} com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar e-mail de adoção.");
        }
    }
}