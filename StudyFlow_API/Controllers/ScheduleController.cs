namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly AnthropicService _service;

    public ScheduleController(AnthropicService service)
    {
        _service = service;
    }

    [HttpPost("process-materials")]
    public async Task<IActionResult> ProcessMaterials()
    {
        var processed = await _service.ProcessAllMaterialsAsync();
        return processed
            ? Ok("All materials processed.")
            : Conflict("Materials already processed.");
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate()
    {
        var generated = await _service.GenerateScheduleAsync();
        return generated  
            ? Ok("Schedule generated.")
            : Conflict("Schedule already covers the current material.");
    }
}