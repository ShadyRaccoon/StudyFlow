namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
    private readonly IActivityRepository _repo;

    public ActivityController(IActivityRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourseId(int courseId) =>
        Ok(await _repo.GetByCourseIdAsync(courseId));

    [HttpPost]
    public async Task<IActionResult> Add(Activity activity)
    {
        await _repo.AddAsync(activity);
        return Created();
    }
}