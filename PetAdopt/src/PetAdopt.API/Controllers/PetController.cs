namespace PetAdopt.API.Controllers;

[ApiController]
[Route("Pet")]
public class PetController(IMediator mediator, ILogger<PetController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<PetController> _logger = logger;


}