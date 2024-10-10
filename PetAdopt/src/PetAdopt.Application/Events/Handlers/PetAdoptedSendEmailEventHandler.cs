using PetAdopt.Application.Events.DTOs;

namespace PetAdopt.Application.Events.Handlers;

public class PetAdoptedSendEmailEventHandler : INotificationHandler<PetAdoptedSendEmailEvent>
{
    //private readonly IEmailService _emailService;
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
            Console.WriteLine($"Handling event: {petAdoptedEvent.Id}");
            // Template Email aqui

            // Envio do e-mail
            //await _emailService.SendEmailAsync(petAdoptedEvent.Contact), subject, body);

            _logger.LogInformation($"Email sent to the adopter {petAdoptedEvent.Name} successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending adoption email");
        }
    }
}