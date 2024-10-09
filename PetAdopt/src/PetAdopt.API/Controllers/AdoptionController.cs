namespace PetAdopt.API.Controllers;

[ApiController]
[Route("Adoption")]
public class AdoptionController(IMediator mediator, ILogger<AdoptionController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<AdoptionController> _logger = logger;

    [HttpPost]
    [Route("AdoptPet")]
    public async Task<ActionResult<bool>> AdoptPet(AdoptPetDTO request)
    {
        try
        {
            _logger.LogInformation("Starting AdoptPet");

            var commandNew = new AdoptPetCommand(request);
            var result = await _mediator.Send(commandNew);

            return Ok(result.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

}