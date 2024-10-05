namespace PetAdopt.API.Controllers;

[ApiController]
[Route("Ngo")]
public class NgoController(IMediator mediator, ILogger<AdoptionController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AdoptionController> _logger = logger;

}