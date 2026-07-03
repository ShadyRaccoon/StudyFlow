namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramScheduleController : ControllerBase
{
    private readonly IProgramScheduleRepository _repo;

    public ProgramScheduleController(IProgramScheduleRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetLatest()
    {
        var schedule = await _repo.GetLatestAsync();
        return schedule is null ? NotFound() : Ok(schedule);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProgramSchedule schedule)
    {
        await _repo.AddAsync(schedule);
        return Created();
    }
}