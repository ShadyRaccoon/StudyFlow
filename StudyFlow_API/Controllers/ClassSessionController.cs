namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassSessionController : ControllerBase
{
    private readonly IClassSessionRepository _repo;

    public ClassSessionController(IClassSessionRepository repo)
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
    public async Task<IActionResult> Add(ClassSession session)
    {
        await _repo.AddAsync(session);
        return Created();
    }
}