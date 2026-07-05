namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _repo;

    public CourseController(ICourseRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _repo.GetByIdAsync(id);
        return course is null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Course course)
    {
        await _repo.AddAsync(course);
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }
}// Controllers/CourseController.cs
