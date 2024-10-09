namespace PetAdopt.API.Controllers;

[ApiController]
[Route("AdoptionForm")]
public class AdoptionFormController(IMediator mediator, ILogger<AdoptionFormController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AdoptionFormController> _logger = logger;


}