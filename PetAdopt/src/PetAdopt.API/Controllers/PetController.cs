using Microsoft.AspNetCore.Authorization;

namespace PetAdopt.API.Controllers;

[ApiController]
[Authorize]
[Route("Pet")]
public class PetController(IMediator mediator, ILogger<PetController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<PetController> _logger = logger;


}