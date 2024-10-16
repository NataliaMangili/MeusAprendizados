using CrossCutting.Logging;

namespace PetAdopt.API.Controllers;

[ApiController]
[Route("Ngo")]
public class NgoController(IMediator mediator, ILogger<NgoController> logger, ILogService logService) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<NgoController> _logger = logger;
    private readonly ILogService _logService = logService;

    [HttpPost]
    [Route("CreateNgo")]
    public async Task<ActionResult<bool>> CreateNgo(CreateNgoRequest request)
    {
        try
        {
            //_logger.LogInformation("Starting CreateNgo");
            await _logService.LogInformation("Starting CreateNgo");

            var commandNew = new CreateNgoCommand(request);
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
