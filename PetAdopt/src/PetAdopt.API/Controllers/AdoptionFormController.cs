namespace PetAdopt.API.Controllers;

[ApiController]
[Route("AdoptionForm")]
public class AdoptionFormController(IMediator mediator, ILogger<AdoptionController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AdoptionController> _logger = logger;

}