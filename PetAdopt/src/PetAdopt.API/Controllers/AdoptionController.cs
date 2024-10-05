namespace PetAdopt.API.Controllers;

[ApiController]
[Route("Adoption")]
public class AdoptionController(IMediator mediator, ILogger<AdoptionController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AdoptionController> _logger = logger;

    [HttpPost]
    [Route("CreateAdoption")]
    public async Task<ActionResult<bool>> CreateAdoption(string request)
    {
        try
        {
            _logger.LogInformation("Iniciando CreateUser");

            var commandNew = new CreateAdoptionCommand(request);
            var result = await _mediator.Send(commandNew);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}