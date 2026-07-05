namespace StudyFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly StudyFlowDbContext _context;

    public AdminController(StudyFlowDbContext context)
    {
        _context = context;
    }

    [HttpDelete("wipe")]
    public async Task<IActionResult> Wipe()
    {
        _context.ProgramSchedules.RemoveRange(_context.ProgramSchedules);
        _context.CourseMaterials.RemoveRange(_context.CourseMaterials);
        _context.Activities.RemoveRange(_context.Activities);
        _context.ClassSessions.RemoveRange(_context.ClassSessions);
        _context.Courses.RemoveRange(_context.Courses);
        await _context.SaveChangesAsync();
        return Ok("Database wiped.");
    }

    [HttpPost("reseed")]
    public async Task<IActionResult> Reseed()
    {
        _context.ProgramSchedules.RemoveRange(_context.ProgramSchedules);
        _context.CourseMaterials.RemoveRange(_context.CourseMaterials);
        _context.Activities.RemoveRange(_context.Activities);
        _context.ClassSessions.RemoveRange(_context.ClassSessions);
        _context.Courses.RemoveRange(_context.Courses);
        await _context.SaveChangesAsync();
        await StudyFlowSeeder.SeedAsync(_context);
        return Ok("Database wiped and reseeded.");
    }
}