namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseMaterialController : ControllerBase
{
    private readonly ICourseMaterialRepository _repo;

    public CourseMaterialController(ICourseMaterialRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var material = await _repo.GetByIdAsync(id);
        return material is null ? NotFound() : Ok(material);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourseId(int courseId) =>
        Ok(await _repo.GetByCourseIdAsync(courseId));

    [HttpPost]
    public async Task<IActionResult> Add(CourseMaterial material)
    {
        await _repo.AddAsync(material);
        return CreatedAtAction(nameof(GetById), new { id = material.Id }, material);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CourseMaterial material)
    {
        if (id != material.Id) return BadRequest();
        await _repo.UpdateAsync(material);
        return NoContent();
    }
}