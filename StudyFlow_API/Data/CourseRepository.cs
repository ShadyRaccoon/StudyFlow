namespace StudyFlow;

public class CourseRepository : ICourseRepository
{
    private readonly StudyFlowDbContext _context;

    public CourseRepository(StudyFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllAsync() =>
        await _context.Courses.ToListAsync();

    public async Task<Course?> GetByIdAsync(int id) =>
        await _context.Courses.FindAsync(id);

    public async Task AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }
}